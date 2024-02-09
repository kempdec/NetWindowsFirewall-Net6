using NetFwTypeLib;
using System.Net;

namespace KempDec.NetWindowsFirewall;

/// <summary>
/// Classe com métodos auxiliares para o Firewall do Windows.
/// </summary>
public class NetWindowsFirewall
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="NetWindowsFirewall"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public NetWindowsFirewall()
    {
        Type firewallType = Type.GetTypeFromProgID("HNetCfg.FwPolicy2")
            ?? throw new InvalidOperationException("Não foi possível obter o tipo da interface do Firewall do " +
                "Windows.");

        INetFwPolicy2 firewall = Activator.CreateInstance(firewallType) as INetFwPolicy2
            ?? throw new InvalidOperationException("Não foi possível criar uma instância do Firewall do Windows.");

        Firewall = firewall;
    }

    /// <summary>
    /// A instância de <see cref="NetWindowsFirewall"/>.
    /// </summary>
    private static NetWindowsFirewall? s_instance;

    /// <summary>
    /// Obtém a instância do Firewall do Windows.
    /// </summary>
    public INetFwPolicy2 Firewall { get; }

    /// <summary>
    /// Obtém a instância de <see cref="NetWindowsFirewall"/>.
    /// </summary>
    public static NetWindowsFirewall Instance => s_instance ??= new();

    /// <summary>
    /// Adiciona uma nova regra de bloqueio (de entrada) para um endereço de IP no Firewall do Windows.
    /// </summary>
    /// <param name="name">O nome da regra de bloqueio a ser adicionada.</param>
    /// <param name="ipAddress">O endereço de IP que será bloqueado na regra de bloqueio a ser adicionada.</param>
    public void AddBlockInIpRule(string name, IPAddress ipAddress) =>
        AddIpRule(name, ipAddress, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN);

    /// <summary>
    /// Adiciona uma nova regra de bloqueio (de entrada e saída) para um endereço de IP no Firewall do Windows.
    /// </summary>
    /// <param name="name">O nome da regra de bloqueio a ser adicionada.</param>
    /// <param name="ipAddress">O endereço de IP que será bloqueado na regra de bloqueio a ser adicionada.</param>
    public void AddBlockIpRule(string name, IPAddress ipAddress)
    {
        AddBlockInIpRule(name, ipAddress);
        AddBlockOutIpRule(name, ipAddress);
    }

    /// <summary>
    /// Adiciona uma nova regra de bloqueio para um endereço de IP no Firewall do Windows.
    /// </summary>
    /// <param name="name">O nome da regra de bloqueio a ser adicionada.</param>
    /// <param name="ipAddress">O endereço de IP que será bloqueado na regra de bloqueio a ser adicionada.</param>
    /// <param name="direction">A direção da regra de bloqueio a ser adicionada.</param>
    public void AddBlockIpRule(string name, IPAddress ipAddress, NET_FW_RULE_DIRECTION_ direction) =>
        AddIpRule(name, ipAddress, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, direction);

    /// <summary>
    /// Adiciona uma nova regra de bloqueio (de saída) para um endereço de IP no Firewall do Windows.
    /// </summary>
    /// <param name="name">O nome da regra de bloqueio a ser adicionada.</param>
    /// <param name="ipAddress">O endereço de IP que será bloqueado na regra de bloqueio a ser adicionada.</param>
    public void AddBlockOutIpRule(string name, IPAddress ipAddress) =>
        AddIpRule(name, ipAddress, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT);

    /// <summary>
    /// Adiciona uma nova regra para um endereço de IP no Firewall do Windows.
    /// </summary>
    /// <param name="name">O nome da regra a ser adicionada.</param>
    /// <param name="ipAddress">O endereço de IP da regra a ser adicionada.</param>
    /// <param name="action">A ação da regra a ser adicionada.</param>
    /// <param name="direction">A direção da regra a ser adicionada.</param>
    /// <param name="description">A descrição da regra a ser adicionada.</param>
    private void AddIpRule(string name, IPAddress ipAddress, NET_FW_ACTION_ action, NET_FW_RULE_DIRECTION_ direction,
        string description = "")
    {
        INetFwRule rule = GetNewRule();

        rule.Action = action;
        rule.Description = description;
        rule.Direction = direction;
        rule.Enabled = true;
        rule.InterfaceTypes = "All";
        rule.Name = name;
        rule.RemoteAddresses = ipAddress.ToString();

        AddRule(rule);
    }

    /// <summary>
    /// Adiciona a regra especificada para o Firewall do Windows.
    /// </summary>
    /// <param name="rule">A regra a ser adicionada para o Firewall do Windows.</param>
    public void AddRule(INetFwRule rule) => Firewall.Rules.Add(rule);

    /// <summary>
    /// Obtém uma nova regra do Firewall do Windows.
    /// </summary>
    /// <returns>A nova regra do Firewall do Windows.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static INetFwRule GetNewRule()
    {
        Type ruleType = Type.GetTypeFromProgID("HNetCfg.FWRule")
            ?? throw new InvalidOperationException("Não foi possível obter o tipo da interface da regra do " +
                "Firewall do Windows.");

        return Activator.CreateInstance(ruleType) as INetFwRule
            ?? throw new InvalidOperationException("Não foi possível criar uma instância da regra do Firewall do " +
                "Windows.");
    }
}
