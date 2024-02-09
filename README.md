# NetWindowsFirewall

NetWindowsFirewall é uma micro biblioetca com métodos auxiliares para gerenciar o Firewall do Windows.

A biblioteca COM `NetFwTypeLib` é usada para gerenciar o Firewall do Windows pelo **NetWindowsFirewall**.

[**Veja NetWindowsFirewall para .NET Framework 4.8**](https://github.com/kempdec/NetWindowsFirewall-Legacy)

## Instalação

1. Instale a biblioteca a partir do NuGet.

``` powershell
Install-Package KempDec.NetWindowsFirewall
```

2. Altere o **"SO de Destino"** do seu projeto para `Windows`.

Para alterar pelo **Visual Studio**, clique com o botão direito no seu projeto e vá em **Propriedades (Alt + Enter)**.
  ![SO de Destino no Visual Studio](assets/target-os.png)


Para alterar por código, abra o arquivo com extensão `.csproj` do seu projeto e adicione `-windows` após a versão do
framework, dentro da tag `<TargetFramework/>`. Exemplo:
  ``` xml
  <!-- Antes. -->
  <TargetFramework>net8.0</TargetFramework>
  <!-- Depois. -->
  <TargetFramework>net8.0-windows</TargetFramework>
  ```

## Como usar

Você pode usar a instância estática, obtida através de `NetWindowsFirewall.Instance`, como no exemplo abaixo:

``` csharp
var ipAddress = IPAddress.Parse("127.0.0.1");

// Adiciona uma regra de bloqueio (de entrada) para o endereço de IP "127.0.1" no Firewall do Windows.
NetWindowsFirewall.Instance.AddBlockInIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Adiciona uma regra de bloqueio (de saída) para o endereço de IP "127.0.0.1" no Firewall do Windows.
NetWindowsFirewall.Instance.AddBlockOutIpRule($"IP {ipAddress} bloqueado", ipAddress);
```

Ou você pode usar a partir de uma nova instância criada manualmente:

``` csharp
var ipAddress = IPAddress.Parse("127.0.0.1");
var netWindowsFirewall = new NetWindowsFirewall();

// Adiciona uma regra de bloqueio (de entrada) para o endereço de IP "127.0.1" no Firewall do Windows.
netWindowsFirewall.AddBlockInIpRule($"IP {ipAddress} bloqueado", ipAddress);

// Adiciona uma regra de bloqueio (de saída) para o endereço de IP "127.0.0.1" no Firewall do Windows.
netWindowsFirewall.AddBlockOutIpRule($"IP {ipAddress} bloqueado", ipAddress);
```

## Colaboração

Ajude-nos a deixar a biblioteca de código aberto cada vez melhor, criando um **pull request**.

## Autores

- [**KempDec**](https://github.com/kempdec) - Mantedora do projeto de código aberto.
- [**Vinícius Lima**](https://github.com/viniciusxdl) - Desenvolvedor .NET C#.

## Licença

[MIT](https://github.com/kempdec/NetWindowsFirewall/blob/main/LICENSE)
