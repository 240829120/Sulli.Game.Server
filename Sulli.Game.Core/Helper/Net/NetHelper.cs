using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 网络辅助类
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// 获取本地IP等信息
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            //本机IP地址 
            string strLocalIP = "";
            //得到计算机名 
            string strPcName = Dns.GetHostName();
            //得到本机IP地址数组 
            IPHostEntry ipEntry = Dns.GetHostEntry(strPcName);
            //遍历数组 
            foreach (var IPadd in ipEntry.AddressList)
            {
                //判断当前字符串是否为正确IP地址 
                if (IsRightIP(IPadd.ToString()))
                {
                    //得到本地IP地址 
                    strLocalIP = IPadd.ToString();
                    //结束循环 
                    break;
                }
            }

            //返回本地IP地址 
            return strLocalIP;
        }
        /// <summary>
        /// 判断是否为正确的IP地址 
        /// </summary>
        /// <param name="strIPadd">IP</param>
        /// <returns></returns>
        public static bool IsRightIP(string strIPadd)
        {
            //利用正则表达式判断字符串是否符合IPv4格式 
            if (Regex.IsMatch(strIPadd, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$"))
            {
                //根据小数点分拆字符串 
                string[] ips = strIPadd.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    //如果符合IPv4规则 
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                    {
                        if (IsPingIP(strIPadd))
                            //正确 
                            return true;
                        else
                            //错误 
                            return false;
                    }

                    //如果不符合 
                    else
                        //错误 
                        return false;
                }
                else
                    //错误 
                    return false;
            }
            else
                //错误 
                return false;
        }

        /// <summary>
        /// 尝试Ping指定IP 是否能够Ping通 
        /// </summary>
        /// <param name="strIP">IP</param>
        /// <returns></returns>
        public static bool IsPingIP(string strIP)
        {
            try
            {
                //创建Ping对象 
                Ping ping = new Ping();
                //接受Ping返回值 
                PingReply reply = ping.Send(strIP, 1000);
                //Ping通 
                return true;
            }
            catch
            {
                //Ping失败 
                return false;
            }
        }

        /// <summary>
        /// 获取第一个可用的端口号
        /// </summary>
        /// <param name="minPort">最小端口号</param>
        /// <param name="maxPort">最大端口号</param>
        /// <returns>可用端口号</returns>
        public static int GetFirstAvailablePort(int minPort, int maxPort)
        {
            for (int i = minPort; i < maxPort; i++)
            {
                if (IsPortAvailable(i))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// 获取操作系统已用的端口号
        /// </summary>
        /// <returns></returns>
        public static IList GetUsedPorts()
        {
            //获取本地计算机的网络连接和通信统计数据的信息
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP)
            {
                allPorts.Add(ep.Port);
            }
            foreach (IPEndPoint ep in ipsUDP)
            {
                allPorts.Add(ep.Port);
            }
            foreach (TcpConnectionInformation conn in tcpConnInfoArray)
            {
                allPorts.Add(conn.LocalEndPoint.Port);
            }

            return allPorts;
        }

        /// <summary>
        /// 检查指定端口是否已用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsPortAvailable(int port)
        {
            bool isAvailable = true;

            IList portUsed = GetUsedPorts();

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }
    }
}
