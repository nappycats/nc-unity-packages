# Nappy Cat • Unity Packages

<div align="center" style="padding:16px;border:1px solid #30363d;border-radius:12px;background:linear-gradient(135deg,#0d1117,#161b22);color:#c9d1d9;">
<div style="font-size:26px;">🐾 Modular tools for building and shipping games</div>
<div style="margin-top:6px;">🚀 Foundation · 🗂️ App Info · 🏷️ Tags · ♻️ Pool · 🧰 Extensions</div>
</div>

## Available Packages

### Free Packages
- 🧱 **com.nappycat.foundation** — Core utilities, Nc.Keys hub, settings management, patterns, and editor tools.
- 🗂️ **com.nappycat.appinfo** — App metadata ScriptableObject (IDs, URLs, packages) with optional Nc.Keys.App constants. Emits to Foundation when present or to `Assets/NappyCat/NcKeys.Generated/` otherwise.
- 🏷️ **com.nappycat.tags** — Hierarchy-aware tags and index builder. Generates `Nc.Keys.Tags.*` when Foundation is present or falls back to `Assets/NappyCat/NcKeys.Generated/`.
- ♻️ **com.nappycat.pool** — Lightweight object and GameObject pooling with optional `Nc.Pool` hub helper and `NcPooledBehaviour` hooks for GameObjects.
- 🧰 **com.nappycat.extensions** — Zero-alloc helpers for math, collections, Unity types, and utility extensions.

### Pro Packages
Pro packages are available in a separate repository for licensed users. Contact us for access.

## Documentation
- Each package includes a `Documentation~/` folder with usage notes and quickstart guides.
- Package links:
  - Foundation: [README](com.nappycat.foundation/README.md), [Docs](com.nappycat.foundation/Documentation~/NcScriptable.Cookbook.md)
  - AppInfo: [README](com.nappycat.appinfo/README.md), [Docs](com.nappycat.appinfo/Documentation~/index.md)
  - Tags: [README](com.nappycat.tags/README.md), [Docs](com.nappycat.tags/Documentation~/index.md)
  - Pool: [README](com.nappycat.pool/README.md), [Docs](com.nappycat.pool/Documentation~/index.md)
  - Extensions: [README](com.nappycat.extensions/README.md), [Docs](com.nappycat.extensions/Documentation~/index.md)

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
