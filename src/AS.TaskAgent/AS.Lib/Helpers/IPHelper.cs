using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace AS.Lib.Helpers
{
    public class IPHelper
    {
        private const int MaxAdapterNameLength = 256;
        private const int MaxAdapterDescriptionLength = 128;
        private const int MaxAdapterAddressLength = 8;
        private const int ERROR_OK = 0;
        private static uint ERROR_BUFFER_OVERFLOW = 111;        
        
        /// <summary>
        /// IP_ADDRESS_STRING struct for "iphlpapi.dll" invocation.
        /// </summary>
        [ComVisible(false), StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal struct IP_ADDRESS_STRING
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string address;
        };

        /// <summary>
        /// IP_MASK_STRING struct for "iphlpapi.dll" invocation.
        /// </summary>
        [ComVisible(false), StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal struct IP_MASK_STRING
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string address;
        };

        /// <summary>
        /// IP_ADDR_STRING struct for "iphlpapi.dll" invocation.
        /// </summary>
        [ComVisible(false), StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal struct IP_ADDR_STRING
        {
            public int Next;      /* struct _IP_ADDR_STRING* */
            public IP_ADDRESS_STRING IpAddress;
            public IP_MASK_STRING IpMask;
            public uint Context;
        };

        [ComVisible(false), StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal struct IP_ADAPTER_INFO
        {
            internal int /* struct _IP_ADAPTER_INFO* */ Next;
            internal uint ComboIndex;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxAdapterNameLength + 4)]
            internal String AdapterName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxAdapterDescriptionLength + 4)]
            internal String Description;
            internal int AddressLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxAdapterAddressLength)]
            internal byte[] Address;
            internal int Index;
            internal int Type;
            internal int DhcpEnabled;
            public uint CurrentIpAddress; /* IP_ADDR_STRING* */
            internal IP_ADDR_STRING IpAddressList;
            internal IP_ADDR_STRING GatewayList;
            internal IP_ADDR_STRING DhcpServer;
            [MarshalAs(UnmanagedType.Bool)]
            internal bool HaveWins;
            internal IP_ADDR_STRING PrimaryWinsServer;
            internal IP_ADDR_STRING SecondaryWinsServer;
            internal uint/*time_t*/ LeaseObtained;
            internal uint/*time_t*/ LeaseExpires;
        };        
        
        [DllImport("iphlpapi", CharSet = CharSet.Auto)]
        private extern static uint GetAdaptersInfo(IntPtr pAdapterInfo, ref int pOutBufLen);        
        
        /// <summary>
        /// Returns an IP_ADAPTER_INFO struct that represents a network adapter on this machine
        /// </summary>
        
        internal static List<IP_ADAPTER_INFO> GetAllAdapterInfo()
        {
            
            List<IP_ADAPTER_INFO> returnResult = new List<IP_ADAPTER_INFO>();
            int size = Marshal.SizeOf(typeof(IP_ADAPTER_INFO));
            IntPtr buffer = Marshal.AllocHGlobal(size);
            uint result = GetAdaptersInfo(buffer, ref size);

            if (result == ERROR_BUFFER_OVERFLOW)
            {
                Marshal.FreeHGlobal(buffer);
                buffer = Marshal.AllocHGlobal(size);
                result = GetAdaptersInfo(buffer, ref size);
            }

            if (result == ERROR_OK)
            {
                int next = (int)buffer;
                IP_ADAPTER_INFO info;
                while (next != 0)
                {
                    info = (IP_ADAPTER_INFO)Marshal.PtrToStructure((IntPtr)next, typeof(IP_ADAPTER_INFO));
                    next = info.Next;
                    returnResult.Add(info);
                }
                Marshal.FreeHGlobal(buffer);
                return returnResult;

            }
            Marshal.FreeHGlobal(buffer);
            throw new InvalidOperationException("GetAllAdapterInfo failed: " + result);
        }

        /// <summary>
        /// Returns an Mac Address as string in hex format. Each hex digit formated with the leading zero
        /// </summary>
        public static string GetMacAddress()
        {
            string returnResult = String.Empty;
            List<IP_ADAPTER_INFO> adapters = GetAllAdapterInfo();

            foreach (IP_ADAPTER_INFO ipAdapterInfo in adapters)
            {
                StringBuilder macAddrText = new StringBuilder();
                int macSumm = 0;
                foreach (byte data in ipAdapterInfo.Address)
                {
                    macSumm += data;
                    macAddrText.AppendFormat("{0:X2}", data);
                }
                if (macSumm > 0)
                {
                    returnResult = macAddrText.ToString();
                    break;
                }
            }
            return returnResult;
        }

        public static string GetHostIpAddress()
        {
            string IpAddress = "127.0.0.1";
            String strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] availableIpList = ipEntry.AddressList;
            foreach (var ip in availableIpList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IpAddress = ip.ToString();
                    break;
                }
            }
            return IpAddress;

        }
    }
}