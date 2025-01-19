Shader "Custom/TextureCoordinates/PortalView" {
    Properties {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _MaskTex("Culling Mask", 2D) = "white" {}
    }
    SubShader {
           Tags { "RenderType" = "Opaque" }

        LOD 200
           CGPROGRAM
           #pragma surface surf Lambert fullforwardshadows addshadow alpha
           #pragma target 3.0
      
          struct Input {
              float4 screenPos;
              float2 uv_MainTex;
              float2 uv_MaskTex;
          };
      
          sampler2D _MainTex;
          sampler2D _MaskTex;
      
          void surf (Input IN, inout SurfaceOutput o) {
              fixed4 c = tex2D(_MainTex, IN.screenPos.xy / IN.screenPos.w);
              o.Emission = c.rgb;
              o.Alpha = tex2D(_MaskTex, IN.uv_MaskTex).a;
          }
          ENDCG
    } 
    Fallback "Diffuse"
}