// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GrayScale" {
  Properties{
    _MainTex("", 2D) = "white" {}
  _Random("Random",Float) = 1.0
    _Range("Range",Float) = 1.0
  }

    SubShader{

    ZTest Always Cull Off ZWrite Off Fog{ Mode Off } //Rendering settings

    Pass{
    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag
    #include "UnityCG.cginc" 
    //we include "UnityCG.cginc" to use the appdata_img struct

    float _Random;
  float _Range;
    struct v2f {
    float4 pos : POSITION;
    half2 uv : TEXCOORD0;
  };
    float random(float2 p) {
      float2 K1 = float2(
        23.14069263277926f, // e^pi (Gelfond's constant)
        2.665144142690225f // 2^sqrt(2) (Gelfondâ€“Schneider constant)
        );
      return cos(dot(p, K1)) * 12345.6789f;
    }
  //Our Vertex Shader 
  v2f vert(appdata_img v) {
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
    return o;
  }

  sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders

                      //Our Fragment Shader
  fixed4 frag(v2f i) : COLOR{
    fixed4 orgCol = tex2D(_MainTex, i.uv); //Get the orginal rendered color 

  float rnd = random(float2(float(orgCol.r), float(orgCol.g)))*_Random;
                                           //Make changes on the color
  fixed avg = fixed((orgCol.r + orgCol.g + orgCol.b) / 3.0f);
  fixed clr = fixed(1 - _Range);
  fixed gry = fixed(_Range);

  //fixed dist = gry * fixed(rnd);

  fixed4 col = fixed4((orgCol.r*clr + avg*gry),
                      (orgCol.g*clr + avg*gry),
                      (orgCol.b*clr + avg*gry), 1);
    
  return col;
  }
    ENDCG
  }
  }
    FallBack "Diffuse"
}