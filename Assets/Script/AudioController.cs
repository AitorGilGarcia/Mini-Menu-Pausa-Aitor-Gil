
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
	[SerializeField] private Slider sliderMusica;
	[SerializeField] private Slider sliderBoton;
	[SerializeField] private AudioSource audioMusica;
	[SerializeField] private AudioSource audioBoton;

    //private float volumenMusica;
    //private float volumenBoton;

    // valores por defecto
    private float volumenMusicaDefault = 0.5f;
    private float volumenBotonDefault = 0.5f;

    // ultimos valores aplicados
    private float volumenMusicaGuardado;
    private float volumenBotonGuardado;


    private void Start()
	{

		// esto lo que hace es coger los valores anteriores guardados 
		volumenMusicaGuardado = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
        volumenBotonGuardado = PlayerPrefs.GetFloat("VolumenBoton", 0.5f);

       // cambia el valor del slider por la variable que guarda el valor aplicado
        sliderMusica.value = volumenMusicaGuardado;
        sliderBoton.value = volumenBotonGuardado;

        // Pone el volumen de la musica segun lo guardado en los playerPrefs
        audioMusica.volume = volumenMusicaGuardado;
        audioBoton.volume = volumenBotonGuardado;


        // Debug.Log("Ruta de guardado: " + Application.persistentDataPath); // ver la ruta del playerPrefs
    }


    public void ReproducirSonido() // este metodo reproduce el sonido del boton
	{
		audioBoton.Play();
	}


    public void SetDefault() // lo que ahce este metodo es que pone los valores los sliders en default, los que ya se han asignado anteriormente
    {
		sliderMusica.value = volumenMusicaDefault; // pone el valor de los slider a default
		sliderBoton.value = volumenBotonDefault;
	}

    public void AplicarCambiosA() // este metodo es el que es activado por el boton Apply, cuando se pulsa este, se aplican los cambios que ha ido guardando
    {
        // pone los valores que se han ido guardando al valor del slider
        volumenMusicaGuardado = sliderMusica.value; 
        volumenBotonGuardado = sliderBoton.value;

        // el audio de la musica es igual al de los valores guardados
        audioMusica.volume = volumenMusicaGuardado;
        audioBoton.volume = volumenBotonGuardado;

        // se guarda en el playerPrefs y lo guarda
        PlayerPrefs.SetFloat("VolumenMusica", volumenMusicaGuardado);
        PlayerPrefs.SetFloat("VolumenBoton", volumenBotonGuardado);
        PlayerPrefs.Save();
    }
    public void CancelarCambiosA() // este metodo es por si el player no guarda los cambios, 
    {
        // el valor de slider es igual al valor guardado anteriormente, como no lo aplica se ponen los antiguos.
		sliderMusica.value = volumenMusicaGuardado;
		sliderBoton.value = volumenBotonGuardado;
	}





	//public void CanviarVolumMusica(float valorMusica) // este metodo lo que hace es el poder cambiar el volumen de la musica utilizando el slider, sin el update
	//{
	//    volumenMusica = valorMusica;
	//    audioMusica.volume = valorMusica;

	//    PlayerPrefs.SetFloat("VolumenMusica", valorMusica);
	//    PlayerPrefs.Save();
	//}

	//public void CanviarVolumBoton(float valorBoton)
	//{
	//    volumenBoton = valorBoton;
	//    audioBoton.volume = valorBoton;

	//    PlayerPrefs.SetFloat("VolumenBoton", valorBoton);
	//    PlayerPrefs.Save();
	//}

}

