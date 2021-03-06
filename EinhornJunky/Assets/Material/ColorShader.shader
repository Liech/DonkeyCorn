// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorBoost" {
  Properties{
    _MainTex("", 2D) = "white" {}
    _Red("Red",Float) = 1.0
    _Green("Green",Float) = 1.0
    _Blue("Blue",Float) = 1.0

  }

    SubShader{

    ZTest Always Cull Off ZWrite Off Fog{ Mode Off } //Rendering settings

    Pass{
    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag
    #include "UnityCG.cginc" 
    //we include "UnityCG.cginc" to use the appdata_img struct

    float _Red;
    float _Green;
    float _Blue;

    struct v2f {
    float4 pos : POSITION;
    half2 uv : TEXCOORD0;
  };

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

                                           //Make changes on the color
  //float avg = (orgCol.r + orgCol.g + orgCol.b) / 3.0f;
  fixed4 col = fixed4(orgCol.r * _Red, orgCol.g * _Green, orgCol.b * _Blue, 1);

  return col;
  }
    ENDCG
  }
  }
    FallBack "Diffuse"
}