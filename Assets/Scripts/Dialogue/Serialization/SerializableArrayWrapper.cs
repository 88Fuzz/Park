using System;

/*
 * JsonUtility cannot serialize a single json array, it must be wrapped in an object first to look like
 * { "someArray": [...] } 
 * http://answers.unity3d.com/questions/1123326/jsonutility-array-not-supported.html
 */
[Serializable]
public class SerializableArrayWrapper<T>
{
    public T[] items;
}