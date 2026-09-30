Shader "Game/Enemy/SlimeWarning"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _WarningProgress ("Warning Progress", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            Tags { "LightMode"="Universal2D" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float _WarningProgress;
            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; float4 color : COLOR; };
            struct v2f { float4 vertex : SV_POSITION; float2 uv : TEXCOORD0; float4 color : COLOR; };
            v2f vert(appdata input)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = input.uv;
                output.color = input.color;
                return output;
            }
            float4 frag(v2f input) : SV_Target
            {
                float4 pixel = tex2D(_MainTex, input.uv) * input.color;
                // 保留亮暗纹理并把绿色亮度转成红色, 避免乘色后预警变成黑影.
                float brightness = max(pixel.r, max(pixel.g, pixel.b));
                pixel.rgb = lerp(pixel.rgb, brightness * float3(1, 0.06, 0.02), saturate(_WarningProgress));
                return pixel;
            }
            ENDCG
        }
    }
}
