# Changelog

All notable changes to this package will be documented in this file.

## [Unreleased]
### Added
- `NC_PKG_FOUNDATION` version define and fallback output path so generated keys land under `Nc.Keys.Tags.*` (Foundation) or `Assets/NappyCat.Generated/` when Foundation is absent.
### Changed
- Key generator now emits to `Nc.Keys.Tags.*` (Nc hub) and menu path updated to `Nappy Cat/Nc Keys/Generate Keys (Tags)`.

## [1.0.0] - 2025-02-15
### Added
- Tag ScriptableObjects (`NcTag`) with hierarchy, color, icon, and synonyms.
- `NcTaggable` component and `NcTagContainer` asset for tagging scene objects and loose assets.
- `NcTagIndex` builder plus `NcTagQuery` runtime helpers (`AnyOf`, `AllOf`, `WhereType`).
- Tag Picker editor window and rebuild menu workflow.
- Documentation, MIT license, and change log scaffolding.
