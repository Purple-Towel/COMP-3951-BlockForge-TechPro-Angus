# BlockForge Feature Test Plan

This test plan focuses on three assigned features before full WinForms implementation is complete:

- Snap grid for workspace block placement
- Variable block data structures for `string`, `int`, and `bool`
- Custom console logging for save feedback

The test suite is intentionally split into:

- Unit tests for pure logic and model behavior that can run now
- Integration or UI placeholder tests for future WinForms event wiring

## Assumptions

- Snap behavior uses a fixed rectangular grid and stores both snapped pixel coordinates and occupied grid row/column.
- Save logic will serialize the snapped block state, not the original raw mouse coordinates.
- Variable blocks are simple data models for this milestone and do not yet require full expression evaluation.
- The custom console can be backed by an in-memory message store even before the actual WinForms console control exists.

## A. Snap Grid Tests

### Properties Testing

- Verify cell width and height are required to be greater than zero.
- Verify the same raw point always produces the same grid cell and snapped location.
- Verify the snapped location is always a multiple of the grid cell size.

### Scenario Based Testing

- Drop a block near the center of a cell and confirm it snaps into the expected row and column.
- Move a block from one grid cell to another and confirm stored grid position updates.
- Save a workspace containing snapped blocks and confirm persisted coordinates match snapped values.

### Functional Testing

- Raw coordinates snap to the nearest valid grid cell.
- Stored grid row and column match the snapped pixel position.
- Out-of-bounds drops clamp safely inside the workspace.

### Edge Case Testing

- Coordinates exactly on grid lines remain on the expected boundary cell.
- Negative coordinates clamp to the origin cell.
- Drops near the right or bottom edge clamp to the last valid cell for the block size.

### User Interaction Testing

- TODO UI hook: workspace drop event calls grid snapping before the block is added to the workspace.
- TODO UI hook: workspace drag move event reapplies snapping when a block is released.

### System Events Testing

- TODO save event hook: saving the workspace uses snapped positions already stored on each block.

## B. Variable Block Tests

### Properties Testing

- Creating `string`, `int`, and `bool` variable blocks stores the correct type.
- Default values are empty string, `0`, and `false`.
- Type-specific update methods reject mismatched value types.

### Scenario Based Testing

- Create several variable blocks, update their values, and serialize them for save.
- Load serialized variable blocks and confirm names, types, and values are preserved.

### Functional Testing

- Variable name, type, and value are stored correctly on creation.
- Empty variable names are rejected.
- Value updates succeed when the block type matches.

### Edge Case Testing

- Empty string values are allowed for string blocks.
- Negative integers and `false` boolean values are preserved correctly.
- TODO duplicate name handling: define whether duplicates are blocked globally or validated elsewhere.

### User Interaction Testing

- TODO UI hook: variable block creation dialog or toolbox action should construct the correct model.
- TODO UI hook: editing a variable block in the properties UI should update the model and refresh the visual state.

### System Events Testing

- Serialization preserves variable type and value across save and load.

## C. Custom Console Tests

### Properties Testing

- Console messages store severity and text.
- Empty or whitespace-only messages are rejected.

### Scenario Based Testing

- A successful save writes `successfully saved workspace` to the console.
- Repeated saves append repeated success messages in order.
- Saving an empty workspace still logs a valid success message.

### Functional Testing

- The notifier dispatches the expected save message to the console sink.
- The console stores appended messages for later display.

### Edge Case Testing

- Logging multiple save messages behaves consistently.
- Logging does not fail when no workspace blocks exist.

### User Interaction Testing

- TODO UI hook: save menu click calls the save service and then appends the success message to the console control.

### System Events Testing

- TODO system event hook: future autosave or keyboard shortcut saves should use the same console logging path.
