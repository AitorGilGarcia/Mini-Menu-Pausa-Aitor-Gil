
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown dropdownResolucion;
	[SerializeField] private Toggle toggleScreen;
    private Resolution[] resoluciones;

	bool isFullscreen = true; // variable si esta en pantalla completa, empieza con true
    int indiceResolucion; // esta variable guarda el idnice de la resolucion que el player esta utilizando en su pantalla
    private int indiceResolucionDefault; //guarda como default el indice de la resolucion que tiene en su pantalla (guarda el valor de indiceResolucion)
    private int resolucionSeleccionada; //variable de que resolucion tiene seleccionada
    private bool fullscreenSeleccionado;

    public void Start()
	{

		resoluciones = Screen.resolutions; // devuelve las resoluciones que el player tiene
		
        dropdownResolucion.ClearOptions(); // elimina las opciones que ya hay en el menu

        HashSet<string> resolucionesUnicas = new HashSet<string>(); // guarda los nombres de las resoluciones ya añadidas.
        foreach (Resolution resolucion in resoluciones) // 
		{
            string nombreResolucion = resolucion.width + " x " + resolucion.height;

            if (resolucionesUnicas.Add(nombreResolucion))
            {
                // esto es para añadir al dropdown la resolucion, pilla la anchura y altura y la indica con nombre
                dropdownResolucion.options.Add(new TMP_Dropdown.OptionData(resolucion.width + " x " + resolucion.height));
                
            }
                
		}

        Resolution resolucionActual = Screen.currentResolution; // coge la resoslucion que tiene el player.
        indiceResolucion = 0;

        bool posicionEncontrada = false;
        for (int i = 0; i < resoluciones.Length && posicionEncontrada == false; i++)
        {
            if (resoluciones[i].width == resolucionActual.width &&
                resoluciones[i].height == resolucionActual.height)
            {
                indiceResolucion = i;
                posicionEncontrada = true;
            }
        }
        indiceResolucionDefault = indiceResolucion; // guardo el indice default para la resolución

        resolucionSeleccionada = indiceResolucion; // indico que se ponga la resolucion del indice
        fullscreenSeleccionado = Screen.fullScreen ; // por defecto, el fullScreen va activado

        dropdownResolucion.value = indiceResolucion; // indico que el dropdown se seleccione el indice de la resolucion que tiene el player actualmente 
        dropdownResolucion.RefreshShownValue(); 

        toggleScreen.isOn = fullscreenSeleccionado;


        dropdownResolucion.onValueChanged.AddListener(CambiarResolucion); // que el dropdown ejecute el metodo de cambiarResolucion cuando deetcte un cambio
        toggleScreen.onValueChanged.AddListener(CambiarFullscreen); // utiliza el metodo CambiarFullscreen cuando detecte un cambio

		Screen.SetResolution(resoluciones[indiceResolucion].width, resoluciones[indiceResolucion].height, isFullscreen); //cambia la resolución de la pantalla, 
        //cogiendo las resoluciones que hay y aplicando la resolución que marca el indiceResolucion
	}
	private void CambiarResolucion(int indice) // este metodo cambia la variable resolucionSeleccionada cuando el usuario seleccione una resolución
	{
        resolucionSeleccionada = indice;
    }

	public void CambiarFullscreen(bool isFullscreen) // este metodo se activa cuando utiliza el toggle, que cambia el valor de fullscreenSeleccionado
	{

        fullscreenSeleccionado = isFullscreen;
    }

    public void SetDefault() // boton de default, pone todos los valores a default, gracias a la variables que han guardado esta información
    {

        resolucionSeleccionada = indiceResolucionDefault;
        fullscreenSeleccionado = true;

        dropdownResolucion.value = resolucionSeleccionada;
        dropdownResolucion.RefreshShownValue();

        toggleScreen.isOn = true;

        Debug.Log("Valores por defecto seleccionados");
    }

    public void AplicarCambiosD() // metodo que se activa cuando el player pulsa el boton de apply, aplica los cambios que ha hehco el player.
    {
        isFullscreen = fullscreenSeleccionado; //aplica si esta en pantalla compelta o no

        Screen.SetResolution(resoluciones[resolucionSeleccionada].width,resoluciones[resolucionSeleccionada].height,isFullscreen); // aplica la resolucion que ah seleccionado
       
        indiceResolucion = resolucionSeleccionada;  // Guarda la ultima configuración aplicada

        Debug.Log("Cambios aplicados");
    }
    public void CancelarCambiosD() // este metodo es cuando el player cierra el menu sin aplicar
    {
        // ponemos los valores como estaban guardados anteriormente.
		resolucionSeleccionada = indiceResolucion;
		fullscreenSeleccionado = isFullscreen;

		dropdownResolucion.value = indiceResolucion;
		dropdownResolucion.RefreshShownValue();

		toggleScreen.isOn = isFullscreen;

		Debug.Log("Cambios cancelados");
    }

}
