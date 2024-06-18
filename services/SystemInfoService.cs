using System;
using System.Runtime.InteropServices;

public class SystemInfoService
{
    public float GetAvailableRAM()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetAvailableRAMWindows();
        }
        else
        {
            throw new PlatformNotSupportedException("Este método no es compatible con la plataforma actual.");
        }
    }

    public float GetTotalRAM()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetTotalRAMWindows();
        }
        else
        {
            throw new PlatformNotSupportedException("Este método no es compatible con la plataforma actual.");
        }
    }

    private float GetAvailableRAMWindows()
    {
        try
        {
            MEMORYSTATUSEX memoryStatus = new MEMORYSTATUSEX();
            if (GlobalMemoryStatusEx(ref memoryStatus))
            {
                return memoryStatus.ullAvailPhys / 1024.0f / 1024.0f; // Convertir bytes a megabytes
            }
            else
            {
                throw new InvalidOperationException("No se pudo obtener la información de memoria.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la RAM disponible: {ex.Message}");
            throw; // Propagar la excepción para que sea manejada en el controlador de la API
        }
    }

    private float GetTotalRAMWindows()
    {
        try
        {
            MEMORYSTATUSEX memoryStatus = new MEMORYSTATUSEX();
            if (GlobalMemoryStatusEx(ref memoryStatus))
            {
                return memoryStatus.ullTotalPhys / 1024.0f / 1024.0f; // Convertir bytes a megabytes
            }
            else
            {
                throw new InvalidOperationException("No se pudo obtener la información de memoria.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la RAM total: {ex.Message}");
            throw; // Propagar la excepción para que sea manejada en el controlador de la API
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX(uint dummy)
        {
            dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            ullTotalPhys = 0;
            ullAvailPhys = 0;
            ullTotalPageFile = 0;
            ullAvailPageFile = 0;
            ullTotalVirtual = 0;
            ullAvailVirtual = 0;
            ullAvailExtendedVirtual = 0;
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);
}
