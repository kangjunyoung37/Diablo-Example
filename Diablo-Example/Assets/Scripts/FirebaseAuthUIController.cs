using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
public class FirebaseAuthUIController : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    public TMP_Text outputText;

    private void Start()
    {
        FireBaseAuthController.Instance.OnChangedLoginState += OnChangedLoginState; // �̺�Ʈ �ڵ鷯 
        FireBaseAuthController.Instance.InitializeFirebase();
    }
    public void CreatrUser()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        FireBaseAuthController.Instance.CreateUser(email, password);
    }
    public void SignIn()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        FireBaseAuthController.Instance.SingIn(email, password);
    }
    public void SignOut()
    {
        FireBaseAuthController.Instance.SignOut();
    }
    public void OnChangedLoginState(bool signedIn)
    {
        outputText.text = signedIn ? "Signed in: " : "Signed out : ";
        outputText.text += FireBaseAuthController.Instance.UserId;
    }

}
