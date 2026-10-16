
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausaController : MonoBehaviour
{
	[SerializeField] private GameObject panelMenuPausa; // metemos el panel del canvas
	[SerializeField] private InputActionReference pauseAction; // el input nuevo 
	[SerializeField] private CanvasGroup canvasGroup; // metemos el canvas Group
    [SerializeField] private DisplayController displayController; // vinculamos el displayController
	[SerializeField] private AudioController audioController; // vinculamos el audioController


    private bool haciendoFade = false; // variable para saber si esta haciendo el fade
	private bool menuAbierto = false; // bool para saber si esta abierto el menu
	private float alphaObjetivo;


	private void Start()
	{
		panelMenuPausa.SetActive(false); //indico que el panel esta desactivado cuando empieze
		canvasGroup.alpha = 0f;
	}



	private void OnEnable() // esto es para que cuando el el menu controller este habilitado, funcione el input
	{
		pauseAction.action.Enable();
	}

	private void OnDisable() // este es lo contrario al OnEnable, si esta desactivado el menuController, no va a funcionar lo del input, ahora estos dos
							//no son super importantes, pero es para un futuro proyecto si quiero reutilizarlo :)
	{
		pauseAction.action.Disable();
	}

	private void CambiarMenu() // metodo que abre y cierra el menu
	{
		menuAbierto = !menuAbierto; // esto invierte el valor del menu, osea que si antes estaba cerrado, pues ahora abierto

		if (menuAbierto) // si es menu esta abierto
		{
			panelMenuPausa.SetActive(true); //se muestra el panel del menu
			Time.timeScale = 0f; // el tiempo pasa a ser de 0, osea que la pelota no se mueve
		}
        else // si se cierra el menu
        {
			// cancela los cambios que ha hecho el player tanto audio como display
            displayController.CancelarCambiosD(); 
			audioController.CancelarCambiosA();
        }
        haciendoFade = true; // hace el fade
	}

	private void Update()
	{
		if (pauseAction.action.WasPressedThisFrame() && !haciendoFade) //comprueba que si pulsa la tecla y no esta haciendo el fade
		{
			CambiarMenu(); // llama al metodo cambiaMenu
		}

		if (haciendoFade) // si esta haciendo el fade
		{
			if (menuAbierto) // si el menu se esta abriendo, aunmenta la transparencia, haciendo el efecto de como que aparece "poco a poco"
			{
				canvasGroup.alpha += Time.unscaledDeltaTime * 3f;
			}
			else // si se esta cerrando, hace lo contrario al anterior, que es restarle la transparencia
			{
				canvasGroup.alpha -= Time.unscaledDeltaTime * 3f;
			}

			if (canvasGroup.alpha >= 1f) // si ya ha hecho el fade, indica que ya hecho con la variable y asegura la transpariencia a 1
			{
				canvasGroup.alpha = 1f;
				haciendoFade = false;
			}

			if (canvasGroup.alpha <= 0f) //  Si el menú deja de ser visible, termina el fade, oculta el panel y vuelve el juego
			{
				canvasGroup.alpha = 0f;
				haciendoFade = false;
				panelMenuPausa.SetActive(false);
				Time.timeScale = 1f; // cuando no esta el menu abierto, el tiempo comienza a correr con normalidad
			}
		}
	}
}
