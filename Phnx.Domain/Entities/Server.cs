using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Entities
{

    public class Server : BaseDeletableEntity
    {
        private Server() { }

        private Server(
            string name,
            ServerType type,
            string provider,
            string os,
            string ipAddress,
            int sshPort,
            string region,
            environment environment
          )
        {
            Name = name;
            Type = type;
            Provider = provider;
            OperatingSystem = os;
            IPAddress = ipAddress;
            SSHPort = sshPort;
            Region = region;
            Environment = environment;

          
        }

        public static Server Create(
            string name,
            ServerType type,
            string provider,
            string os,
            string ipAddress,
            int sshPort,
            string region,
            environment environment,
            string username,
            string encryptedPassword,
            string sshKey,
            string notes)
            => new(name, type, provider, os, ipAddress, sshPort, region, environment );

        public string Name { get; private set; } = string.Empty;
        public ServerType Type { get; private set; } =ServerType.Cloud;
        public string Provider { get; private set; } = string.Empty;
        public string OperatingSystem { get; private set; } = string.Empty;
        public string IPAddress { get; private set; } = string.Empty;
        public int SSHPort { get; private set; }
        public string Region { get; private set; } = string.Empty;
        public environment? Environment { get; private set; } 

       

        public void Update(
            string name,
            ServerType type,
            string provider,
            string os,
            string ipAddress,
            int sshPort,
            string region,
            environment environment
           )
        {
            Name = name;
            Type = type;
            Provider = provider;
            OperatingSystem = os;
            IPAddress = ipAddress;
            SSHPort = sshPort;
            Region = region;
            Environment = environment;

            
        }
    }
}