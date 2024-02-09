using KempDec.NetWindowsFirewall;
using System.Net;

var ipAddress = IPAddress.Parse("127.0.0.1");

// Exemplo com a instância estática de NetWindowsFirewall:
// -----------------------------------------------------------------------------------------------------

// Adiciona uma regra de bloqueio (de entrada e saída) para o endereço de IP "127.0.1" no Firewall do Windows.
NetWindowsFirewall.Instance.AddBlockIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Adiciona uma regra de bloqueio (de entrada) para o endereço de IP "127.0.1" no Firewall do Windows.
NetWindowsFirewall.Instance.AddBlockInIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Adiciona uma regra de bloqueio (de saída) para o endereço de IP "127.0.0.1" no Firewall do Windows.
NetWindowsFirewall.Instance.AddBlockOutIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Exemplo com criação de uma instância manual:
// -----------------------------------------------------------------------------------------------------

var netWindowsFirewall = new NetWindowsFirewall();

// Adiciona uma regra de bloqueio (de entrada e saída) para o endereço de IP "127.0.1" no Firewall do Windows.
netWindowsFirewall.AddBlockIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Adiciona uma regra de bloqueio (de entrada) para o endereço de IP "127.0.1" no Firewall do Windows.
netWindowsFirewall.AddBlockInIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Adiciona uma regra de bloqueio (de saída) para o endereço de IP "127.0.0.1" no Firewall do Windows.
netWindowsFirewall.AddBlockOutIpRule($"IP {ipAddress} bloqueado", ipAddress);
