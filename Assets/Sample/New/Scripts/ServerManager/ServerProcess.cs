using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ServerProcess : MonoBehaviour
{
    private string[] rowsTarget;
    private string[] formFields, formValues;
    private string codeReadDataRows = "";

    private enum TargetRead
    {
        None,
        Register_Soul_Card,
        Read_Umum,
        Read_All_Table_1,
        Write_All_Table_Value_20,
        Write_All_Table_Value_Fix
    }

    private TargetRead targetRead = TargetRead.None;

    public void ReadDataRows(string codePhp, string phpAction, string[] fields, string[] values, UnityAction<string[]> resultAction)
    {
        codeReadDataRows = codePhp;
        formFields = fields;
        formValues = values;
        SetTargetRead(phpAction);

        if (targetRead != TargetRead.None)
        {
            StartCoroutine(ReadDataRowsCoroutine(resultAction));
        }
        else
        {
            Debug.LogError("No target specified for access.");
        }
    }

    private void SetTargetRead(string action)
    {
        switch (action)
        {
            case "Register_Soul_Card":
                targetRead = TargetRead.Register_Soul_Card;
                break;
            case "Read_Umum":
                targetRead = TargetRead.Read_Umum;
                break;
            case "Read_All_Table_1":
                targetRead = TargetRead.Read_All_Table_1;
                break;
            case "Write_All_Table_Value_20":
                targetRead = TargetRead.Write_All_Table_Value_20;
                break;
            case "Write_All_Table_Value_Fix":
                targetRead = TargetRead.Write_All_Table_Value_Fix;
                break;
            default:
                targetRead = TargetRead.None;
                break;
        }
    }

    private IEnumerator ReadDataRowsCoroutine(UnityAction<string[]> resultAction)
    {
        
        if (targetRead == TargetRead.None)
        {
            Debug.LogError("No target specified for access.");
            yield break;
        }

        yield return new WaitForSeconds(0.05f);

        using (UnityWebRequest request = CreateRequest())
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                ConfirmFailure(resultAction);
                yield break;
            }

            ProcessResponse(request.downloadHandler.text, resultAction);
        }
    }

    private UnityWebRequest CreateRequest()
    {
        WWWForm phpForm = new WWWForm();
        for (int i = 0; i < formFields.Length; i++)
        {
            phpForm.AddField(formFields[i], formValues[i]);
        }

        string url = $"{HiGameManager.serverManager.phpLink}/{targetRead}.php";
        Debug.Log (url);
        return UnityWebRequest.Post(url, phpForm);
    }

    private void ProcessResponse(string response, UnityAction<string[]> resultAction)
    {
        rowsTarget = response.Split(';');

        if (rowsTarget.Length > 1)
        {
            ConfirmSuccess(resultAction);
        }
        else
        {
            Debug.Log("Read Error");
            ConfirmFailure(resultAction);
        }
    }

    private void ConfirmSuccess(UnityAction<string[]> resultAction)
    {
        rowsTarget[0] = "Success";
        resultAction?.Invoke(rowsTarget); // Callback for success
        Destroy(this.gameObject);
    }

    private void ConfirmFailure(UnityAction<string[]> resultAction)
    {
        rowsTarget = new string[] { "Failed" };
        resultAction?.Invoke(rowsTarget); // Callback for failure
        Destroy(this.gameObject);
    }
}
