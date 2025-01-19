Shader "MobileWater/MobileWater" {
Properties {
 _Color ("Water Colour", Color) = (1,1,1,1)
 _MainTex ("Water Texture", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
SubProgram "gles " {
"!!GLES

#ifndef SHADER_API_GLES
    #define SHADER_API_GLES 1
#endif
#ifndef SHADER_API_MOBILE
    #define SHADER_API_MOBILE 1
#endif
#line 17

        varying mediump vec2 uv;
                     
        
#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform highp mat4 glstate_matrix_mvp;
#define gl_Vertex _glesVertex
attribute vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
attribute vec4 _glesMultiTexCoord0;

        uniform mediump vec4 _MainTex_ST;
        void main() {
   gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
            uv = gl_MultiTexCoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
        }
  
#endif
#ifdef FRAGMENT

        uniform lowp sampler2D _MainTex;
        uniform lowp vec4 _Color;
        void main() {
            gl_FragColor = texture2D(_MainTex, uv) * _Color;
        }

        
#endif"
}
}
 }
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * constant }
 }
}
}