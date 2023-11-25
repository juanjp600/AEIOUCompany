using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Text;

namespace AEIOU_Company;
public static class TTS
{
    static readonly int BUFFER_SIZE = 8192;
    private static NamedPipeClientStream _namedPipeClientStream;
    private static StreamWriter _streamWriter;
    private static StreamReader _streamReader;
    private static BinaryReader _binaryReader;
    private static bool _initialized = false;
    public static void Init()
    {
        StartSpeakServer();
        try
        {
            _namedPipeClientStream = new NamedPipeClientStream("AEIOUCOMPANYMOD");
            _streamWriter = new StreamWriter(_namedPipeClientStream, Encoding.UTF8, BUFFER_SIZE, true);
            _streamReader = new StreamReader(_namedPipeClientStream, Encoding.UTF8, false, BUFFER_SIZE, true);
            _binaryReader = new BinaryReader(_namedPipeClientStream, Encoding.UTF8, true);
        }
        catch (IOException e)
        {
            Plugin.LogError(e);
        }
        _initialized = true;
    }
    public static void Speak(string message)
    {
        if (!_initialized)
        {
            Plugin.LogError("Tried to speak before initializing TTS!");
            return;
        }
        try
        {
            if (!_namedPipeClientStream.IsConnected)
            {
                Plugin.Log("ConnectingToPipe");
                _namedPipeClientStream.Connect();
            }
            Plugin.Log($"Sending: msgA={message}]");
            _streamWriter.WriteLine($"msgA={message}]"); // ] to close off any accidentally opened talk commands
            _streamWriter.Flush();
        }
        catch (IOException e)
        {
            Plugin.LogError("Speak" + e);
        }
    }
    public static float[] SpeakToMemory(string message)
    {
        if (!_initialized)
        {
            Plugin.LogError("Tried to speak before initializing TTS!");
            return default(float[]);;
        }
        try
        {
            if (!_namedPipeClientStream.IsConnected)
            {
                Plugin.Log("ConnectingToPipe");
                _namedPipeClientStream.Connect();
            }
            Plugin.Log($"Sending: msg={message}]");
            _streamWriter.WriteLine($"msg={message}]"); // ] to close off any accidentally opened talk commands
            _streamWriter.Flush();

            char[] buffer = new char[BUFFER_SIZE*64];
            int nextCharacter = -2;
            while (nextCharacter != -1  || nextCharacter != 0)
            {
                int i = 0;
                _streamReader.ReadBlock(buffer, i * BUFFER_SIZE, BUFFER_SIZE);
                nextCharacter = _streamReader.Peek();
                i++;
                Plugin.Log("reading stream: " + nextCharacter);
            }
            byte[] bytesBuffer = Encoding.UTF8.GetBytes(buffer);
            float[] floatBuffer = new float[BUFFER_SIZE * 8];
            for (int i = 0; i < floatBuffer.Length; i++)
            {
                floatBuffer[i] = BitConverter.ToSingle(bytesBuffer, i*4);
                Plugin.Log(floatBuffer[i]);
            }
            return floatBuffer;
        }
        catch (IOException e)
        {
            Plugin.LogError("Speak" + e);
        }
        return default(float[]);
    }
    private static void StartSpeakServer()
    {
        if (CheckForAEIOUSPEAKProcess())
        {
            return;
        }
        string directory = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath.Replace($"{PluginInfo.PLUGIN_NAME}.dll", "");
        Plugin.Log(directory + "AEIOUSpeak.exe");
        Process speakServerProcess = Process.Start(directory + "AEIOUSpeak.exe");
        if (speakServerProcess == null)
        {
            Plugin.LogError("Failed to start Speak Server");
        }
    }
    private static bool CheckForAEIOUSPEAKProcess()
    {
        Process[] processes = Process.GetProcessesByName("AEIOUSpeak");
        if (processes.Length > 0)
        {
            return true;
        }
        return false;
    }
}