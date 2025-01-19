Shader "Portal/Mask/Projector" {
Properties {
 _MainTex ("Cookie", 2D) = "gray" { TexGen ObjectLinear }
 _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}
SubShader { 
 Tags { "QUEUE"="Geometry-100" }
 Pass {
  Tags { "QUEUE"="Geometry-100" }
  AlphaToMask On
  ZTest Less
  AlphaTest Greater [_Cutoff]
  ColorMask A
  Offset -1, -1
  SetTexture [_MainTex] { Matrix [_Projector] combine texture }
 }
}
}