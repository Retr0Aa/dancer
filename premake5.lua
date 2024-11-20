workspace "Dancer"
   configurations { "Debug", "Release" }

   project "Dancer"
    kind "ConsoleApp"
    language "C#"
    location "Dancer"

    files {
        "Dancer/**.cs"
    }

    removefiles {
        "Dancer/bin/**.*",
        "Dancer/obj/**.*"
    }

    links {
        "System",
        "System.Drawing",
        "System.Windows.Forms"
    }

    nuget {
        "Microsoft.Win32.Registry:4.7.0",
        "NAudio:2.2.1",
        "NAudio:Asio.2.2.1",
        "NAudio:Core.2.2.1",
        "NAudio:Midi.2.2.1",
        "NAudio:Wasapi.2.2.1",
        "NAudio:WinForms.2.2.1",
        "NAudio:WinMM.2.2.1",
        "System.Security.AccessControl:4.7.0",
        "System.Security.Principal.Windows:4.7.0",
    }
