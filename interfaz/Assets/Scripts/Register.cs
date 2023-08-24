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
    public TMP_InputField usuario, contrasenia, email, rcontrasenia, edad;
    public TextMeshProUGUI msgbox, msgbox2, msgbox3, msgbox4, msgbox5, msgbox6;
    char[] CaracteresEspaciales = { '!', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };

    void Start()
    {
        contrasenia.onValueChanged.AddListener(OnContraseniaValueChanged);
        contrasenia.onValueChanged.AddListener(OnRContraseniaValueChanged);
        rcontrasenia.onValueChanged.AddListener(OnRContraseniaValueChanged);
        email.onValueChanged.AddListener(OnEmailValueChanged);
        edad.onValueChanged.AddListener(OnEdadValueChanged);
        usuario.onValueChanged.AddListener(OnUsuarioValueChanged);

        SocketManager.instancia.socket.OnUnityThread("registro", (response) =>
        {
            Debug.Log(response);
            Dictionary<string, string> res = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.ToString().Split('[')[1].Split(']')[0]);

            if (res["msg"] == "exito") msgbox.text = "Se registro correctamente, ahora inicie sesi�n";
            else if (res["msg"] == "EyN")
            {
                msgbox3.text = "El correo ingresado est� en uso";
                msgbox6.text = "El usuario ingresado est� en uso";
            }
            else if (res["msg"] == "email") msgbox3.text = "El correo ingresado est� en uso";
            else if (res["msg"] == "nombre") msgbox6.text = "El usuario ingresado est� en uso";
            else msgbox.text = "No se pudo registrar, verifique los datos ingresados";
        });
    }

    public void Enviar()
    {
        if (!ValidarDatos())
            return;
        SocketManager.instancia.socket.Emit("registro", new {datos = new string[]{usuario.text.ToLower(),edad.text,contrasenia.text,rcontrasenia.text,email.text.ToLower()}});
    }

    private bool ValidarDatos()
    {

        if (string.IsNullOrEmpty(usuario.text) || string.IsNullOrEmpty(edad.text) || string.IsNullOrEmpty(contrasenia.text) || string.IsNullOrEmpty(rcontrasenia.text) || string.IsNullOrEmpty(email.text))
        {
            //msgbox.text = "Todos los campos son obligatorios";
            return false;
        }

        if (usuario.text.Length > 20 || usuario.text.Length < 4)
        {
            //msgbox.text = "El usuario debe tener entre 4 y 20 caracteres";
            return false;
        }

        if (!int.TryParse(Regex.Replace(edad.text.Trim(), "[^0-9]", ""), out int edadNum) || edadNum > 99 || edadNum < 5)
        {
            //msgbox.text = "Ingrese una edad v�lida";
            return false;
        }

        if (ValidarFormatoCorreo(email.text) == false)
        {
            //msgbox.text = "Formato de correo electr�nico inv�lido";
            return false;
        }

        if (ValidarSeguridadContrasenia(contrasenia.text).Any(c => c != null))
        {
            //msgbox.text = $"La contrase�a al menos debe tener: {string.Join(", ", ValidarSeguridadContrasenia(contrasenia.text).Where(c => c != null))}";
            return false;
        }

        if (contrasenia.text != rcontrasenia.text)
        {
            //msgbox.text = "Las contrase�as no coinciden";
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
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
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

        string[] sugerencias = {
        Regex.IsMatch(contrasenia, @"[A-Z]") ? null : "Una may�scula",
        Regex.IsMatch(contrasenia, @"[a-z]") ? null : "Una min�scula",
        Regex.IsMatch(contrasenia, @"\d") ? null : "Un n�mero",
        contrasenia.Any(c => CaracteresEspaciales.Contains(c)) ? null : "Un caracter especial",
        contrasenia.Length >= 9 && contrasenia.Length <= 20 ? null : "Entre 9 y 20 caracteres"
    };
        return sugerencias;
    }

    public void irAlLogin()
    {
        SceneManager.LoadScene("interfaz_inicio_sesion");
    }

    private void OnUsuarioValueChanged(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            msgbox6.text = "Campo obligatorio";
        }
        else if (usuario.text.Length > 20 || usuario.text.Length < 4)
        {
            msgbox6.text = "El usuario debe tener entre 4 y 20 caracteres";
        }
        else
        {
            msgbox6.text = "";
        }
    }

    private void OnEdadValueChanged(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            msgbox5.text = "Campo obligatorio";
        }
        else if (!int.TryParse(Regex.Replace(newValue.Trim(), "[^0-9]", ""), out int edadNum) || edadNum > 99 || edadNum < 5)
        {
            msgbox5.text = "Ingrese una edad v�lida";
        }
        else
        {
            msgbox5.text = "";
        }
    }

    private void OnRContraseniaValueChanged(string newValue)
    {
        newValue.Trim();

        if (string.IsNullOrEmpty(newValue))
        {
            msgbox4.text = "Campo obligatorio";
        }
        else if (contrasenia.text != rcontrasenia.text)
        {
            msgbox4.text = "Las contrase�as no coinciden";
        }        
        else
        {
            msgbox4.text = "";
        }

    }

    private void OnEmailValueChanged(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            msgbox3.text = "Campo obligatorio";
        }
        else if (!ValidarFormatoCorreo(newValue))
        {
            msgbox3.text = "Formato de correo electr�nico inv�lido";
        }
        else
        {
            msgbox3.text = "";
        }
    }

    private void OnContraseniaValueChanged(string newValue)
    {
        newValue = newValue.Trim();

        if (string.IsNullOrEmpty(newValue))
        {
            msgbox2.text = "Campo obligatorio";
        }
        else if (!Regex.IsMatch(newValue, @"[A-Z]"))
        {
            msgbox2.text = "La contrase�a al menos debe tener una may�scula";
        }
        else if (!Regex.IsMatch(newValue, @"[a-z]"))
        {
            msgbox2.text = "La contrase�a al menos debe tener una minuscula";
        }
        else if (!newValue.Any(c => CaracteresEspaciales.Contains(c)))
        {
            msgbox2.text = "La contrase�a al menos debe tener un caracter especial";
        }
        else if (!Regex.IsMatch(newValue, @"\d"))
        {
            msgbox2.text = "La contrase�a al menos debe tener un n�mero";
        }
        else if (newValue.Length < 9 || newValue.Length > 20)
        {
            msgbox2.text = "La contrase�a al menos debe tener entre 9 y 20 caracteres";
        }
        else
        {
            msgbox2.text = "";
        }
    }
}
