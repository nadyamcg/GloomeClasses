# GlooMeClasses v1.2.3 Changelog

an update has been released for GlooMeClasses! I appreciate everyone's patience and willingness to help me with debugging and fixing their experiences :>

## Fixed

### Refurbished Crock System
- **Rosin Sealing**: refurbished crocks can now be sealed as intended
  - added 3D seal model ([assets/gloomeclasses/shapes/block/clay/crock/seal.json](assets/gloomeclasses/shapes/block/clay/crock/seal.json))
  - rosin provides better preservation than fat/beeswax sealing
  - recipe updated to support `rosinSealed` attribute
  - `OnCreatedByCrafting` method handles sealing attributes and copies crock contents properly
  - **Attribute Handling**: fixed attribute handling when crafting sealed crocks
    - properly copies attributes from input crock to output
    - correctly sets `sealed` and `rosinSealed` attributes based on sealing ingredient

- **Crock Crafting Patch**: new Harmony patch to allow vanilla crocks in refurbished crock conversion recipes
  - you can craft from vanilla to refurbished crocks using a knife as intended
  - properly handles sealing ingredient detection

### Class System
- **Class Migration System**: automatic migration from vanilla classes to GloomeClasses
  - migrates players who had vanilla classes before installing the mod
  - players receive in-game notification when their class is migrated
  - migration events are logged to diagnostics
  - new `LogClassMigration` method tracks player class migrations

- **`.charsel` Crash Prevention**: using `.charsel` in certain conditions should no longer crash your game
  - added validation in [CharacterSystemDiagnosticPatches.cs](src/Diagnostics/Patches/CharacterSystemDiagnosticPatches.cs#L29) to detect invalid/disabled class codes
  - prevents crashes when players have classes that no longer exist
  - provides clear warning messages and diagnostic logging
  - common when adding GloomeClasses to existing saves with vanilla classes
  - prefix patch now returns `false` to skip original method when class is invalid

### Class-Specific Fixes
- **Locust Lover: Metalbit Healing Refactor**: fixes issues with tin bronze metal bits becoming unworkable due to a conflict with SmithingPlus
  - metalbit healing now uses runtime configuration instead of JSON patches
  - simplified [apply-healhacked-behavior-patch.json](assets/gloomeclasses/patches/apply-healhacked-behavior-patch.json) to single behavior entry
  - healing values determined by metalbit variant at runtime:
    - tin bronze: 2 HP (non-corrupted healer)
    - black bronze: 4 HP (corrupted healer)
  - more maintainable and extensible architecture

### System Improvements
- **Logging System Overhaul**:
  - all debug logging in [BlockEntityMetalBarrel.cs](src/Alchemist/BlockEntityMetalBarrel.cs) converted to use `Log` utility
  - debug output now respects enable/disable flags:
    - server debug logging disabled by default (reduces spam)
    - client debug logging enabled by default

- **Diagnostics System Improvements**:
  - string comparisons now use `StringComparison.CurrentCultureIgnoreCase` instead of `.ToLower()`
  - more efficient and proper case-insensitive string matching in [DiagnosticLogger.cs](src/Diagnostics/DiagnosticLogger.cs)
  - better detection of mod conflicts and class mods

---