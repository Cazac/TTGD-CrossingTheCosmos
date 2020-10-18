using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

///////////////
/// <summary>
///     
/// The RARC_Serializer class is used to serizalie data and sotre it after the game is powered off or between scences
/// 
///     //Usage Examples
///     Serializer.Save<ExampleClass>(filenameWithExtension, exampleClass);
///     ExampleClass exampleClass = Serializer.Load<ExampleClass>(filenameWithExtension));
/// 
/// </summary>
///////////////

public class RARC_Serializer
{
    ///////////////////////////////////////////////////////////////// - Loading Files

    public static T Load<T>(string filename) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                using (Stream stream = File.OpenRead(filename))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as T;
                }
            }
            catch (Exception e)
            {
                //Error
                Debug.Log(e.Message);
            }
        }
        return default(T);
    }

    ///////////////////////////////////////////////////////////////// - Saving Files

    public static void Save<T>(string filename, T data) where T : class
    {
        using (Stream stream = File.OpenWrite(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    /////////////////////////////////////////////////////////////////

    public static void DeleteFile(string filename)
    {
        string filePath = Application.dataPath + "/../" + filename;

        //Check if file exists
        if (File.Exists(filePath))
        {
            //Delete File
            File.Delete(filePath);
        }
        else
        {
            //Debug.Log("Test Code: No File");
        }

    }

    /////////////////////////////////////////////////////////////////
}