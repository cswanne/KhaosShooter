﻿using System.Xml.Serialization;
using System.IO;

public static class Assistant
//the purpose of this assistant script is to handle tasks for me

{

    public static bool gameOver = false;
    public static int currentFuel = 1000;
    public static int currentAmmo = 100;
    public static float canisterDestroyTime = 0f;

    public static void updateFuel(int value)
    {
        currentFuel += value;
        if (currentFuel < 0) currentFuel = 0;
    }


    //Serilise
    public static string Serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }



    //Deserialise
    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }
	
}
