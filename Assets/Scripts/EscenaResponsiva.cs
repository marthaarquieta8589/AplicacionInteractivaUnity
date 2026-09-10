using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EscenaResponsiva : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void IniciarEscena()
    {
        GameObject controlador = new GameObject("ControladorEscena");
        controlador.AddComponent<EscenaResponsiva>();
    }

    void Start()
    {
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

        CrearInterfaz(canvasObj.transform);
    }

    void CrearInterfaz(Transform padre)
    {
        CrearTexto("APLICACIÓN INTERACTIVA", padre,
            new Vector2(0.25f, 0.82f),
            new Vector2(0.75f, 0.95f), 40);

        CrearBoton("INICIO", padre,
            new Vector2(0.35f, 0.58f),
            new Vector2(0.65f, 0.68f));

        CrearBoton("INFORMACIÓN", padre,
            new Vector2(0.35f, 0.45f),
            new Vector2(0.65f, 0.55f));

        CrearBoton("SALIR", padre,
            new Vector2(0.35f, 0.32f),
            new Vector2(0.65f, 0.42f));
    }

    void CrearBoton(string nombre, Transform padre, Vector2 min, Vector2 max)
    {
        GameObject botonObj = new GameObject(nombre, typeof(RectTransform));
        botonObj.transform.SetParent(padre, false);

        Image imagen = botonObj.AddComponent<Image>();
        imagen.color = new Color(0.15f, 0.45f, 0.85f);

        Button boton = botonObj.AddComponent<Button>();

        RectTransform rect = botonObj.GetComponent<RectTransform>();
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        CrearTexto(nombre, botonObj.transform,
            Vector2.zero, Vector2.one, 24);

        boton.onClick.AddListener(() =>
            Debug.Log("Botón presionado: " + nombre));
    }

    void CrearTexto(string contenido, Transform padre,
        Vector2 min, Vector2 max, int tamaño)
    {
        GameObject textoObj = new GameObject("Texto", typeof(RectTransform));
        textoObj.transform.SetParent(padre, false);

        Text texto = textoObj.AddComponent<Text>();
        texto.text = contenido;
        texto.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        texto.fontSize = tamaño;
        texto.alignment = TextAnchor.MiddleCenter;
        texto.color = Color.white;

        RectTransform rect = textoObj.GetComponent<RectTransform>();
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    void CrearEventSystem()
    {
        GameObject evento = new GameObject("EventSystem");
        evento.AddComponent<EventSystem>();
        evento.AddComponent<StandaloneInputModule>();
    }
}

