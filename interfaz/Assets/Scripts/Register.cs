using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using System.Text;

class Formulario
{
    public string nombre { get; set; }
    public string edad { get; set; }
    public string contrasenia { get; set; }
    public string rcontrasenia { get; set; }
    public string correo { get; set; }
}

public class Register : MonoBehaviour
{
    public TextMeshProUGUI usuario, contrasenia, email, rcontrasenia, edad, msgbox;

    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("registro", (response) =>
        {
            Dictionary<string, bool> res = JsonConvert.DeserializeObject<Dictionary<string, bool>>(response.ToString().Split('[')[1].Split(']')[0]);
            if (res["exito"]) msgbox.text = "Se registro correctamente, ahora inicie sesion";
            else msgbox.text = "No se pudo registrar, verifique los datos ingresados";
        });
    }

    public void Enviar()
    {

        if (!ValidarDatos())
            return;

        Formulario form = new Formulario
        {
            nombre = usuario.text,
            edad = edad.text,
            contrasenia = contrasenia.text,
            rcontrasenia = rcontrasenia.text,
            correo = email.text
        };
        SocketManager.instancia.socket.Emit("registro", JsonConvert.SerializeObject(form));
    }

    private bool ValidarDatos()
    {
        if (string.IsNullOrEmpty(usuario.text) || string.IsNullOrEmpty(edad.text) || string.IsNullOrEmpty(contrasenia.text) || string.IsNullOrEmpty(rcontrasenia.text) || string.IsNullOrEmpty(email.text))
        {
            msgbox.text = "Todos los campos son obligatorios";
            return false;
        }

        if (!int.TryParse(Regex.Replace(edad.text.Trim(), "[^0-9]", ""), out int edadNum) || edadNum > 99 || edadNum < 5)
        {
            msgbox.text = "Ingrese una edad válida";
            return false;
        }

        if (ValidarFormatoCorreo(email.text) == false)
        {
            msgbox.text = "El formato del correo electrónico es inválido";
            return false;
        }

        if (ValidarSeguridadContrasenia(contrasenia.text).Any(c => c != null))
        {
            msgbox.text = $"La contraseña al menos debe tener:\n{string.Join("\n", ValidarSeguridadContrasenia(contrasenia.text).Where(c => c != null))}";
            return false;
        }

        if (contrasenia.text != rcontrasenia.text)
        {
            msgbox.text = "Las contraseñas no coinciden";
            return false;
        }

        return true;
    }

    private bool ValidarFormatoCorreo(string email)
    {
        email = email.Trim();

        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();

                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException )
        {
            return false;
        }
        catch (ArgumentException )
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }

    }

    private string[] ValidarSeguridadContrasenia(string contrasenia)
    {
        contrasenia = contrasenia.Trim();

        char[] CaracteresEspaciales = { '!', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };

        string[] sugerencias = {
        Regex.IsMatch(contrasenia, @"[A-Z]") ? null : "Una mayúscula",
        Regex.IsMatch(contrasenia, @"[a-z]") ? null : "Una minúscula",
        Regex.IsMatch(contrasenia, @"\d") ? null : "Un número",
        contrasenia.Any(c => CaracteresEspaciales.Contains(c)) ? null : "Un caracter especial",
        contrasenia.Length >= 9 && contrasenia.Length <= 20 ? null : "Entre 9 y 20 caracteres"
    };
        return sugerencias;
    }


    public void irAlLogin()
    {
        SceneManager.LoadScene("interfaz_inicio_sesion");
    }

    /* string inputText = edad.text.Trim();

         for (int i = 0; i < inputText.Length; i++)
         {
             Debug.Log("Carácter " + i + ": " + inputText[i] + " (Unicode: " + ((int)inputText[i]) + ")");
         }

         Debug.Log("Comparación: " + (inputText == "12")); */

    /* string inputText = Regex.Replace(edad.text.Trim(), "[^0-9]", ""); // Eliminar caracteres no numéricos

 Debug.Log("Comparación: " + (inputText == "12"));*/
}
