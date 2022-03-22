using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sulli.Game.Storage;
using Sulli.Game.Storage.Layer;
using Sulli.Game.Mock;
using Sulli.Game.Server;
using Sulli.Game.Server.Connection.Http;
using Sulli.Game.Server.Connection.WebSocket;
using System;
using System.Threading.Tasks;
using Sulli.Game.Core;
using Autofac;

namespace Sulli.Game.UnitTest.Server
{
    /// <summary>
    /// 服务测试
    /// </summary>

    [TestClass]
    public class ServerTest
    {
        /// <summary>
        /// HTTP测试
        /// </summary>
        [TestMethod]
        public void HttpPostTest()
        {
            // Application | IOC
            Application.Current.ContainerBuilder.RegisterType<StudentController>();

            Application.Current.BuildIoc();
            Application.Current.BuildAsync().Wait();

            // Server
            ServerOption serverOption = new ServerOption();
            serverOption.ControllerRegisterContainer.Create("Sulli.Game.Mock").All();
            ServerEngine engine = new ServerEngine(serverOption);

            // -------------------------------------------------------------------------------------------
            // Connection
            HttpConnectionOption connectionOption = new HttpConnectionOption();
            connectionOption.UriPrefixs.Add("http://localhost:5000/");
            HttpConnection connection = new HttpConnection(engine, connectionOption);
            engine.Connections.Add(connection);

            engine.BuildAsync().Wait();
            engine.StartAsync();

            Task.Delay(1000).Wait();
            // -------------------------------------------------------------------------------------------

            Student? student = Sulli.Game.Core.HttpHelper.PostAsync<Student>(new System.Uri("http://localhost:5000/Student/Query")).Result;

            Assert.IsNotNull(student);
            Assert.IsTrue(student.Name == "zhangsan");
        }

        /// <summary>
        /// WebSocket测试
        /// </summary>
        [TestMethod]
        public void WebSocketTest()
        {
            // Application | IOC
            Application.Current.ContainerBuilder.RegisterType<StudentController>();

            Application.Current.BuildIoc();
            Application.Current.BuildAsync().Wait();

            // Server
            ServerOption serverOption = new ServerOption();
            serverOption.ControllerRegisterContainer.Create("Sulli.Game.Mock").All();
            ServerEngine engine = new ServerEngine(serverOption);

            // -------------------------------------------------------------------------------------------
            // Connection
            WebSocketConnectionOption connectionOption = new WebSocketConnectionOption();
            connectionOption.UriPrefixs.Add("ws://localhost:5001/");
            WebSocketConnection connection = new WebSocketConnection(engine, connectionOption);
            engine.Connections.Add(connection);

            engine.BuildAsync().Wait();
            engine.StartAsync();

            Task.Delay(1000).Wait();
            // -------------------------------------------------------------------------------------------
            WebSocketClient client = new WebSocketClient("ws://localhost:5001/");
            client.WebSocket.Connect();

            Student? student = client.CallAsync<Student>("ws://localhost:5001/Student/Query", null, null).Result;

            Assert.IsNotNull(student);
            Assert.IsTrue(student.Name == "zhangsan");
        }
    }
}
