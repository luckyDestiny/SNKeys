using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace SNKeys
{
    class Program
    {
        static void Main(string[] args)
        {
			var sysNo_HostNameWithMac = GetMD5("YGwkfk5" + HostName(withDomainName: false) + GetMacAddr() + "HOlWmPR3");
			var snNo_HostNameWithMac =  GetMD5("U6Xb8qY5" + sysNo_HostNameWithMac + "q7wf3l2");
			Console.WriteLine($"sysNo_HostNameWithMac:{ sysNo_HostNameWithMac }");
			Console.WriteLine($" snNo_HostNameWithMac:{ snNo_HostNameWithMac }");

			Console.WriteLine();
		
			var sysNo_Mac = GetMD5("YGwkfk5" + GetMacAddr() + "HOlWmPR3");
			var snNo_Mac =  GetMD5("U6Xb8qY5"+ sysNo_Mac + "q7wf3l2");
			Console.WriteLine($"sysNo_mac:{ sysNo_Mac }");
			Console.WriteLine($" snNo_mac:{ snNo_Mac }");

			Console.ReadLine();
        }


		static string GetMD5(string p)
		{
			try
			{
				return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(p))).Replace("-", string.Empty);
			}
			catch (Exception)
			{
				return "";
			}
		}
		static string HostName(bool withDomainName)
		{
			try
			{
				IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
				return withDomainName ? $"{iPGlobalProperties.HostName}.{iPGlobalProperties.DomainName}" : iPGlobalProperties.HostName;
			}
			catch
			{
				return string.Empty;
			}
		}

		static string GetMacAddr()
		{
			try
			{
				IPGlobalProperties.GetIPGlobalProperties();
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				if (allNetworkInterfaces == null || allNetworkInterfaces.Length < 1)
				{
					return "";
				}
				NetworkInterface[] array = allNetworkInterfaces;
				for (int i = 0; i < array.Length; i++)
				{
					PhysicalAddress physicalAddress = array[i].GetPhysicalAddress();
					if (physicalAddress.ToString() != null)
					{
						return physicalAddress.ToString();
					}
				}
			}
			catch (Exception)
			{
			}
			return "";
		}
	}

	

}
