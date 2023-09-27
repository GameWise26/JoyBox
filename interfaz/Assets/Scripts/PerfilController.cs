using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleFileBrowser;

public class PerfilController : MonoBehaviour
{
    private byte[] imagen;
    public Image fdpXD, fdpHead;
    public TextMeshProUGUI amigos, nombre;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("imagenCargada", (response) =>{
            List<string> res = SocketManager.instancia.pasarLista(response);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imagen);

            SocketManager.instancia.fdp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            fdpXD.sprite = SocketManager.instancia.fdp;
            fdpHead.sprite = SocketManager.instancia.fdp;
        });
        fdpXD.sprite = SocketManager.instancia.fdp;
        amigos.text = "Amigos " + (SocketManager.instancia.amigos.Count/3).ToString();
        nombre.text = SocketManager.instancia.nombre;
    }

    public void LoadImage()
    {
        /*OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Archivos de imagen|*.jpg;*.png;*.bmp;*.jpeg|Todos los archivos|*.*";

        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            string imagePath = fileDialog.FileName;
            imagen = File.ReadAllBytes(imagePath);
            SocketManager.instancia.socket.Emit("cargarImagen",new {datos = imagen});
        }*/
		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Images", ".jpg", ".png" ), new FileBrowser.Filter( "Text Files", ".txt", ".pdf" ) );
		FileBrowser.SetDefaultFilter( ".jpg" );
		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );
		FileBrowser.AddQuickLink( "Users", "C:\\Users", null );
		StartCoroutine( ShowLoadDialogCoroutine() );
    }
    
	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load" );

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log( FileBrowser.Success );

		if( FileBrowser.Success )
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for( int i = 0; i < FileBrowser.Result.Length; i++ )
				Debug.Log( FileBrowser.Result[i] );

			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            imagen = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0]);
			SocketManager.instancia.socket.Emit("cargarImagen",new {datos = imagen});

			// Or, copy the first file to persistentDataPath
			/*string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
			FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );*/
		}
	}
}

