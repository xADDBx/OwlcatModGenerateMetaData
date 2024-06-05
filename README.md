### OwlcatModGenerateMetaData

## Usage

Reference the package:
```
<PackageReference Include="OwlcatModGenerateMetaData" IncludeAssets="runtime; build; native; contentfiles; analyzers; buildtransitive" Version="1.0.9" PrivateAssets="all" />
```

Add Target with Task. Integrate it however you want; here as AfterBuild Target as an example:
```
<Target Name="Publish" AfterTargets="AfterBuild">
    <OwlcatModGenerateMetaData PathToManifest="$(MSBuildThisFileDirectory)\ModDetails\OwlcatModificationManifest.json" ImageDir="$(MSBuildThisFileDirectory)\ModDetails\" BuildDir="$(SolutionDir)$(OutDir)" GameAppId="2186680" />
</Target>/>
```

Arguments:

- `PathToManifest`: Absolute path to the the OwlcatModificationManifest.json
- `ImageDir`: Absolute path to the directory containing the image file. The name of the image file is specified in OwlcatModificationManifest.json
- `BuildDir`: Directory containing the mod files
- `GameAppId`: AppId of the game you want to publish to, defaults to 2186680 which is Warhammer 40,000: Rogue Trader