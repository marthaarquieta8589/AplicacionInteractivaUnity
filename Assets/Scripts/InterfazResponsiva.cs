using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InterfazResponsiva : MonoBehaviour
{
    Font fuente;
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
static void IniciarInterfaz()
{
    GameObject controlador = new GameObject("ControladorInterfaz");
    controlador.AddComponent<InterfazResponsiva>();
}
    void Start()
    {
        fuente = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        CrearEventSystem();

        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        canvasObj.AddComponent<GraphicRaycaster>();

        CrearPanelPrincipal(canvasObj.transform);
    }

    void CrearPanelPrincipal(Transform padre)
    {
        GameObject panel = CrearUI("PanelPrincipal", padre);

        Image fondo = panel.AddComponent<Image>();
        fondo.color = new Color(0.08f, 0.11f, 0.18f, 1f);

        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        CrearTexto(
            "APLICACIÓN INTERACTIVA",
            panel.transform,
            new Vector2(0.2f, 0.82f),
            new Vector2(0.8f, 0.96f),
            42
        );

        CrearBoton("INICIO", panel.transform,
            new Vector2(0.35f, 0.58f),
            new Vector2(0.65f, 0.68f));

        CrearBoton("INFORMACIÓN", panel.transform,
            new Vector2(0.35f, 0.45f),
            new Vector2(0.65f, 0.55f));

        CrearBoton("SALIR", panel.transform,
            new Vector2(0.35f, 0.32f),
            new Vector2(0.65f, 0.42f));

        CrearIconos(panel.transform);
        CrearEnlaces(panel.transform);
    }

    void CrearIconos(Transform padre)
    {
        string[] iconos = { "1", "2", "3", "4" };

        for (int i = 0; i < 4; i++)
        {
            float x1 = 0.32f + (i * 0.10f);
            float x2 = x1 + 0.07f;

            GameObject icono = CrearUI("Icono" + (i + 1), padre);

            Image imagen = icono.AddComponent<Image>();
            imagen.color = new Color(0.12f, 0.55f, 0.95f, 1f);

            RectTransform rect = icono.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(x1, 0.15f);
            rect.anchorMax = new Vector2(x2, 0.25f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            CrearTexto(iconos[i], icono.transform,
                Vector2.zero, Vector2.one, 26);
        }
    }

    void CrearEnlaces(Transform padre)
    {
        CrearEnlace("Unity", "https://unity.com",
            padre, new Vector2(0.03f, 0.65f),
            new Vector2(0.20f, 0.72f));

        CrearEnlace("GitHub", "https://github.com",
            padre, new Vector2(0.03f, 0.52f),
            new Vector2(0.20f, 0.59f));

        CrearEnlace("Documentación", "https://docs.unity3d.com",
            padre, new Vector2(0.03f, 0.39f),
            new Vector2(0.20f, 0.46f));

        CrearEnlace("Google", "https://www.google.com",
            padre, new Vector2(0.80f, 0.65f),
            new Vector2(0.97f, 0.72f));

        CrearEnlace("YouTube", "https://www.youtube.com",
            padre, new Vector2(0.80f, 0.52f),
            new Vector2(0.97f, 0.59f));

        CrearEnlace("UDG", "https://www.udg.mx",
            padre, new Vector2(0.80f, 0.39f),
            new Vector2(0.97f, 0.46f));
    }

    void CrearBoton(string nombre, Transform padre,
        Vector2 min, Vector2 max)
    {
        GameObject botonObj = CrearUI(nombre, padre);

        Image imagen = botonObj.AddComponent<Image>();
        imagen.color = new Color(0.10f, 0.45f, 0.85f, 1f);

        Button boton = botonObj.AddComponent<Button>();

        RectTransform rect = botonObj.GetComponent<RectTransform>();
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        CrearTexto(nombre, botonObj.transform,
            Vector2.zero, Vector2.one, 24);

        boton.onClick.AddListener(() =>
            Debug.Log("Botón seleccionado: " + nombre));
    }

    void CrearEnlace(string texto, string url,
        Transform padre, Vector2 min, Vector2 max)
    {
        GameObject botonObj = CrearUI(texto, padre);

        Image imagen = botonObj.AddComponent<Image>();
        imagen.color = new Color(0.15f, 0.18f, 0.25f, 0.95f);

        Button boton = botonObj.AddComponent<Button>();

        RectTransform rect = botonObj.GetComponent<RectTransform>();
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        CrearTexto(texto, botonObj.transform,
            Vector2.zero, Vector2.one, 20);

        boton.onClick.AddListener(() =>
            Application.OpenURL(url));
    }

    void CrearTexto(string contenido, Transform padre,
        Vector2 min, Vector2 max, int tamaño)
    {
        GameObject textoObj = CrearUI("Texto", padre);

        Text texto = textoObj.AddComponent<Text>();
        texto.text = contenido;
        texto.font = fuente;
        texto.fontSize = tamaño;
        texto.alignment = TextAnchor.MiddleCenter;
        texto.color = Color.white;

        RectTransform rect = textoObj.GetComponent<RectTransform>();
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    GameObject CrearUI(string nombre, Transform padre)
    {
        GameObject objeto = new GameObject(
            nombre,
            typeof(RectTransform)
        );

        objeto.transform.SetParent(padre, false);

        return objeto;
    }

    void CrearEventSystem()
    {
        GameObject evento = new GameObject("EventSystem");
        evento.AddComponent<EventSystem>();
        evento.AddComponent<StandaloneInputModule>();
    }
}
