Shader "Custom/InvertedSphere"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Background" }
        Cull Front // Désactive le rendu des faces externes et garde l'intérieur
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz = -v.vertex.xyz; // Inverse la direction des sommets
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
