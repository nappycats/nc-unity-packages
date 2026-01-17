# Changelog

All notable changes to this package will be documented in this file.

## [1.0.1] - 2026-01-14
### Added
- Extension to `Nc` hub for easy access to app info via `Nc.AppInfo`.
### Changed
- Separated `NcAppInfo` (ScriptableObject) and `NcApp` (accessor) into separate files for better organization.
- Marked `NcApp` as obsolete; use `Nc.AppInfo` instead.

## [1.0.0] - 2025-02-15
### Added
- `NcAppInfo` ScriptableObject for app identity, URLs, and package metadata.
- `NcApp` static accessor that loads shared info from Resources.
- `NcAppConstsGenerator` editor utility and generated constants file (`NcKeys.App.g.cs`).
- Project Settings integration for editing app info inline.
- Documentation, MIT license, and change log scaffolding.
