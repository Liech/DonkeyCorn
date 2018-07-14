// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Outline"
{
  Properties
  {
    [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Color("Tint", Color) = (1,1,1,1)
    [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
    
    _Outline("outline",Float) = 1.0

    // Add values to determine if outlining is enabled and outline color.
    [PerRendererData] _Outline("Outline", Float) = 0
    [PerRendererData] _OutlineColor("Outline Color", Color) = (1,1,1,1)
  }

    SubShader
  {
    Tags
  {
    "Queue" = "Transparent"
    "IgnoreProjector" = "True"
    "RenderType" = "Transparent"
    "PreviewType" = "Plane"
    "CanUseSpriteAtlas" = "True"
  }

    Cull Off
    Lighting Off
    ZWrite Off
    Blend One OneMinusSrcAlpha

    Pass
  {
    CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile _ PIXELSNAP_ON
#pragma shader_feature ETC1_EXTERNAL_ALPHA
#include "UnityCG.cginc"

    struct appdata_t
  {
    float4 vertex   : POSITION;
    float4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
  };

  struct v2f
  {
    float4 vertex   : SV_POSITION;
    fixed4 color : COLOR;
    float2 texcoord  : TEXCOORD0;
  };

  fixed4 _Color;    
  fixed2 _Flip;
  float _Outline;
  fixed4 _OutlineColor;

  inline float4 UnityFlipSprite(in float3 pos, in fixed2 flip)
  {
    return float4(pos.xy * flip, pos.z, 1.0);
  }

  v2f vert(appdata_t IN)
  {
    v2f OUT;

    UNITY_SETUP_INSTANCE_ID(IN);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

    OUT.vertex = IN.vertex;
    OUT.vertex = UnityObjectToClipPos(OUT.vertex);
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color;

#ifdef PIXELSNAP_ON
    OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

    return OUT;
  }

  sampler2D _MainTex;
  sampler2D _AlphaTex;
  float4 _MainTex_TexelSize;

  fixed4 SampleSpriteTexture(float2 uv)
  {
    fixed4 color = tex2D(_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
    // get the color from an external texture (usecase: Alpha support for ETC1 on android)
    color.a = tex2D(_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA

    return color;
  }

  fixed4 frag(v2f IN) : SV_Target
  {
    float3 result = tex2D(_MainTex, IN.texcoord).rgb;
    float a = tex2D(_MainTex, IN.texcoord).a;
    
  // If outline is enabled and there is a pixel, try to draw an outline.
  if (_Outline > 0) {
    // Get the neighbouring four pixels.

    float pixelUp    = tex2D(_MainTex, IN.texcoord + fixed2(0, _MainTex_TexelSize.y/2)).a;
    float pixelDown  = tex2D(_MainTex, IN.texcoord - fixed2(0, _MainTex_TexelSize.y/2)).a;
    float pixelRight = tex2D(_MainTex, IN.texcoord + fixed2(_MainTex_TexelSize.x/2, 0)).a;
    float pixelLeft  = tex2D(_MainTex, IN.texcoord - fixed2(_MainTex_TexelSize.x/2, 0)).a;


    // If one of the neighbouring pixels is invisible, we render an outline.
    if ((pixelUp   <= 0.2 && pixelUp    >= 0.05) ||
        (pixelDown <= 0.2 && pixelDown  >= 0.05) ||
        (pixelRight<= 0.2 && pixelRight >= 0.05) ||
        (pixelLeft <= 0.2 && pixelLeft  >= 0.05) 
      ) {
      a = 1;
      result.rgb = float3(0, 0, 0);
      //result.rgba = float4()
      //c.rgba = fixed4(0, 0, 0, 1) * _Outline + c * (1- _Outline);
    }
  }

  result *= a;

  return fixed4(result.r,result.g,result.b,a);
  //return c;
  }
    ENDCG
  }
  }
}