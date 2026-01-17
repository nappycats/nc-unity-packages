# NappyCat Unity Packages

A collection of Unity packages for game development, providing modular tools and frameworks.

## Available Packages

### Free Packages
- **com.nappycat.foundation** — Core utilities, Nc.Keys hub, settings management, patterns, and editor tools.
- **com.nappycat.appinfo** — App metadata ScriptableObject (IDs, URLs, packages) with optional Nc.Keys.App constants. Emits to Foundation when present or to `Assets/NappyCat/NcKeys.Generated/` otherwise.
- **com.nappycat.tags** — Hierarchy-aware tags and index builder. Generates `Nc.Keys.Tags.*` when Foundation is present or falls back to `Assets/NappyCat/NcKeys.Generated/`.

### Pro Packages
Pro packages are available in a separate repository for licensed users. Contact us for access.

## Installation

1. Open your Unity project.
2. Go to `Window > Package Manager`.
3. Click the `+` button and select `Add package from git URL`.
4. Enter the package URL: `https://github.com/nappycats/nc-unity-packages.git#[package-name]`

Example: `https://github.com/nappycats/nc-unity-packages.git#com.nappycat.foundation`
Another example: `https://github.com/nappycats/nc-unity-packages.git#com.nappycat.appinfo`

## License

Most packages are free and open-source. Pro packages require a separate license.

## Support

For issues or questions, visit [our website](http://nappycat.net) or open an issue on GitHub.
