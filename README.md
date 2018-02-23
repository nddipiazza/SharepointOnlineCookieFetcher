# SharepointOnlineCookieFetcher
Generates the SPOIDCRL for SharePoint online

# Building

**Linux:**
```
wget https://github.com/nddipiazza/SharepointOnlineCookieFetcher/archive/master.zip
unzip master.zip
cd SharepointOnlineCookieFetcher-master
nuget restore
xbuild SharepointOnlineCookieFetcher.sln
cd SharepointOnlineCookieFetcher
mkbundle --simple --static --deps -L ../packages/Microsoft.SharePointOnline.CSOM.16.1.7317.1200/lib/net45 -L ../packages/Utility.CommandLine.Arguments.1.3.0/lib -o ./bin/Debug/SharepointOnlineSecurityUtil --config /etc/mono/config --machine-config /etc/mono/4.5/machine.config ./bin/Debug/SharepointOnlineCookieFetcher.exe
```

**OSX:**
```
wget https://github.com/nddipiazza/SharepointOnlineCookieFetcher/archive/master.zip
unzip master.zip
cd SharepointOnlineCookieFetcher-master
nuget restore
xbuild SharepointOnlineCookieFetcher.sln
cd SharepointOnlineCookieFetcher
mkbundle --simple --static --deps -L ../packages/Microsoft.SharePointOnline.CSOM.16.1.7317.1200/lib/net45 -L ../packages/Utility.CommandLine.Arguments.1.3.0/lib -o ./bin/Debug/SharepointOnlineSecurityUtil --config /Library/Frameworks/Mono.framework/Versions/5.4.1/etc/mono/config --machine-config /Library/Frameworks/Mono.framework/Versions/5.4.1/etc/mono/4.5/machine.config ./bin/Debug/SharepointOnlineCookieFetcher.exe
```

Builds `SharepointOnlineCookieFetcher/bin/Debug/SharepointOnlineSecurityUtil`

# Usage

One Time Only Step for Linux users that do not have Mono-runtime installed: (see https://stackoverflow.com/questions/24872394/access-to-the-path-etc-mono-registry-is-denied)

```
mkdir /some/path/mono-registry
chmod uog+rw /some/path/mono-registry
```

Failure to do this will result in `System.Security.SecurityException: No access to the given key ---> System.UnauthorizedAccessException: Access to the path "/etc/mono/registry" is denied.`

Run program:

**Linux and OSX:**
`MONO_REGISTRY_PATH=/some/path/mono-registry ./SharepointOnlineSecurityUtil -u youruser@yourdomain.com -w https://tenant.sharepoint.com`

**Windows:**
`SharepointOnlineSecurityUtil.exe -u youruser@yourdomain.com -w https://tenant.sharepoint.com`

Enter a password when promped

Result of stdout will have `SPOIDCRL` cookie.
