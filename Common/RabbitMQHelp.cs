using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Common
{
    public class MyDefaultBasicConsumer : DefaultBasicConsumer 
    {
        public MyDefaultBasicConsumer(IModel model) : base(model)
        {
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var msg = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine("收到消息：" + msg);
            base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);
        }
    }

    public class RabbitMQHelp
    {
        // 创建连接工厂
        public static ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = "127.0.0.1",// 设置主机
            Port = 5672,    // 设置端口
            VirtualHost = "/ems",   //设置虚拟主机
            // 设置可以访问该虚拟主机的用户名和密码
            UserName = "ems",
            Password = "123"
        };

        private static IConnection GetConnection()
        {
            try
            {
                // 获取连接对象
                var connection = factory.CreateConnection();
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }

        private static IModel GetChannel(IConnection connection)
        {
            var channel = connection.CreateModel();
            return channel;
        }

        /// <summary>
        /// 生产者（模式1,2）直连
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="msg"></param>
        public static void Provider(string queue,params string[] msgs)
        {
            using (var connection = GetConnection())
            {
                // 获取连接通道
                using (var channel = GetChannel(connection))
                {
                    // 将通道和消息队列绑定 
                    // 参数1 : 队列名称，没有的话就自动创建一个队列
                    // 参数2 : 是否要持久化
                    // 参数3 ：是否独占队列
                    // 参数4:：是否自动删除
                    // 参数5 ：需要额外传递的附加参数
                    channel.QueueDeclare(queue, false, false, false, null);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = false;
                    // 设置请求头
                    //basicProperties.Headers = new Dictionary<string, object>();
                    //basicProperties.Headers.Add("latitude", 51.5252949);
                    //var body = Encoding.UTF8.GetBytes(message);
                    foreach (string msg in msgs)
                    {
                        // 参数1 : 交换机名称
                        // 参数2 : 路由key
                        // 参数3 ：传递消息的额外设置
                        // 参数4 ：传递的消息
                        channel.BasicPublish("", queue, properties, Encoding.UTF8.GetBytes(msg));// 发送消息
                    }
                }
            }
        }
        /// <summary>
        /// 生产者（模式3）广播
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="msgs"></param>
        public static void Provider3(string exchange, params string[] msgs)
        {
            using (var connection = GetConnection())
            {
                // 获取连接通道
                using (var channel = GetChannel(connection))
                {
                    // 参数1 : 交换机名称 
                    // 参数2 : 交换机类型 fanout 广播
                    channel.ExchangeDeclare(exchange, "fanout");
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = false;
                    // 设置请求头
                    //basicProperties.Headers = new Dictionary<string, object>();
                    //basicProperties.Headers.Add("latitude", 51.5252949);
                    //var body = Encoding.UTF8.GetBytes(message);
                    foreach (string msg in msgs)
                    {
                        // 参数1 : 交换机名称
                        // 参数2 : 路由key，当为exchange="fanou"时,无效
                        // 参数3 ：传递消息的额外设置
                        // 参数4 ：传递的消息
                        channel.BasicPublish(exchange, "", properties, Encoding.UTF8.GetBytes(msg));// 发送消息
                    }
                }
            }
        }

        /// <summary>
        /// 生产者（模式4）路由
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="msgs"></param>
        public static void Provider4(string exchange,string routingKey, params string[] msgs)
        {
            using (var connection = GetConnection())
            {
                // 获取连接通道
                using (var channel = GetChannel(connection))
                {
                    // 参数1 : 交换机名称 
                    // 参数2 : 交换机类型 direct 路由下的直连
                    channel.ExchangeDeclare(exchange, "direct");// ***
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = false;
                    // 设置请求头
                    //basicProperties.Headers = new Dictionary<string, object>();
                    //basicProperties.Headers.Add("latitude", 51.5252949);
                    //var body = Encoding.UTF8.GetBytes(message);
                    foreach (string msg in msgs)
                    {
                        // 参数1 : 交换机名称
                        // 参数2 : 路由key，当为exchange="fanou"时,无效
                        // 参数3 ：传递消息的额外设置
                        // 参数4 ：传递的消息
                        channel.BasicPublish(exchange, routingKey, properties, Encoding.UTF8.GetBytes(msg));// ***
                    }
                }
            }
        }

        /// <summary>
        /// 消费者（模式1）直连
        /// 不要关闭连接和通道
        /// 否则只能消费一次消息
        /// </summary>
        /// <param name="queue"></param>
        public static void Customer1(string queue)
        {
            var connection = GetConnection();
            // 获取连接通道
            var channel = GetChannel(connection);
            // 将通道和消息队列绑定 
            // 参数1 : 队列名称，没有的话就自动创建一个队列
            // 参数2 : 是否要持久化
            // 参数3 ：是否独占队列
            // 参数4:：是否自动删除
            // 参数5 ：需要额外传递的附加参数
            channel.QueueDeclare(queue, false, false, false, null);
            // 参数1 : 队列
            // 参数2 ：自动应答
            // 参数3 ：接收消息对象
            channel.BasicConsume(queue, true, new MyDefaultBasicConsumer(channel));// 接收消息
        }

        /// <summary>
        /// 消费者（模式2）
        /// 不要关闭连接和通道
        /// 否则只能消费一次消息
        /// </summary>
        /// <param name="queue"></param>
        public static void Customer2(string queue)
        {
            var connection = GetConnection();
            // 获取连接通道
            var channel = GetChannel(connection);
            // 将通道和消息队列绑定 
            // 参数1 : 队列名称，没有的话就自动创建一个队列
            // 参数2 : 是否要持久化
            // 参数3 ：是否独占队列
            // 参数4:：是否自动删除
            // 参数5 ：需要额外传递的附加参数
            channel.QueueDeclare(queue, false, false, false, null);
            // 限流设置 在autoAck = true 的时候无效
            // prefetchSize: 单条消息的大小限制，Con通常设置为0，表示不做限制
            // refetchCount: 一次最多能处理多少条消息
            // global: 是否将上面设置true应用于channel级别还是取false代表Con级别
            channel.BasicQos(0, 1, false);

            // 回调
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                Thread.Sleep(1000);
                Console.WriteLine(" [x] Done");
                // deliveryTag 消息确认标志
                // multiple 是否进行多个确认
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            // 参数1 : 队列
            // 参数2 ：自动应答
            // 参数3 ：接收消息对象
            channel.BasicConsume(queue, false, consumer);// 接收消息
        }

        /// <summary>
        /// 消费者（模式3）广播
        /// 不要关闭连接和通道
        /// 否则只能消费一次消息
        /// </summary>
        /// <param name="exchange">交换机的名称</param>
        public static void Customer3(string exchange)
        {
            var connection = GetConnection();
            // 获取连接通道
            var channel = GetChannel(connection);
            // 将通道和消息队列绑定 
            // 参数1 : 队列名称，没有的话就自动创建一个队列
            // 参数2 : 是否要持久化
            // 参数3 ：是否独占队列
            // 参数4:：是否自动删除
            // 参数5 ：需要额外传递的附加参数
            channel.ExchangeDeclare(exchange, "fanout");
            // 限流设置 在autoAck = true 的时候无效
            // prefetchSize: 单条消息的大小限制，Con通常设置为0，表示不做限制
            // refetchCount: 一次最多能处理多少条消息
            // global: 是否将上面设置true应用于channel级别还是取false代表Con级别
            channel.BasicQos(0, 1, false);

            // 回调
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [广播] Received {0}", message);
                Thread.Sleep(1000);
                Console.WriteLine(" [广播] Done");
                // deliveryTag 消息确认标志
                // multiple 是否进行多个确认
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            //获取临时队列
            string queueName = channel.QueueDeclare().QueueName;
            //绑定交换机
            channel.QueueBind(queueName, exchange, "");

            // 参数1 : 队列
            // 参数2 ：自动应答
            // 参数3 ：接收消息对象
            channel.BasicConsume(queueName, false, consumer);// 接收消息
        }

        /// <summary>
        /// 消费者（模式4）路由
        /// 不要关闭连接和通道
        /// 否则只能消费一次消息
        /// </summary>
        /// <param name="exchange">交换机的名称</param>
        public static void Customer4(string exchange, params string[] routingKeys)
        {
            var connection = GetConnection();
            // 获取连接通道
            var channel = GetChannel(connection);
            // 将通道和消息队列绑定 
            // 参数1 : 队列名称，没有的话就自动创建一个队列
            // 参数2 : 是否要持久化
            // 参数3 ：是否独占队列
            // 参数4:：是否自动删除
            // 参数5 ：需要额外传递的附加参数
            channel.ExchangeDeclare(exchange, "direct");
            // 限流设置 在autoAck = true 的时候无效
            // prefetchSize: 单条消息的大小限制，Con通常设置为0，表示不做限制
            // refetchCount: 一次最多能处理多少条消息
            // global: 是否将上面设置true应用于channel级别还是取false代表Con级别
            channel.BasicQos(0, 1, false);

            // 回调
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [广播] Received {0}", message);
                Thread.Sleep(1000);
                Console.WriteLine(" [广播] Done");
                // deliveryTag 消息确认标志
                // multiple 是否进行多个确认
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            //获取临时队列
            string queueName = channel.QueueDeclare().QueueName;

            //绑定交换机与路由key
            foreach (var routingKey in routingKeys)
            {
                channel.QueueBind(queueName, exchange, routingKey);
            }

            // 参数1 : 队列
            // 参数2 ：自动应答
            // 参数3 ：接收消息对象
            channel.BasicConsume(queueName, false, consumer);// 接收消息
        }
    }
}
