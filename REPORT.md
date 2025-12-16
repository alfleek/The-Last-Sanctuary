# REPORT.md

## Challenges & Fixes

### 1. Inventory Items Not Appearing After Crafting
**Problem:**  
Crafted items were removed from the inventory materials but did not appear in the inventory UI.

**Cause:**  
The crafted item prefab could not be found using `Resources.Load()`, causing it to return `null`.

**Fix:**  
- Ensured all craftable item prefabs exist inside a `Resources/` folder  
- Matched `craftedItemName` exactly with prefab file names  
- Added null checks and debug logs before instantiating items  

---

### 2. Ingredients Removed But Still Visible in Inventory
**Problem:**  
Ingredients were removed logically but still appeared visually in inventory slots.

**Cause:**  
Inventory data updated without removing the corresponding UI GameObjects.

**Fix:**  
- Destroyed slot child objects when items are removed  
- Synced inventory logic with UI representation  

---

### 3. Craft Button Not Enabling Correctly
**Problem:**  
Craft button stayed disabled even when enough materials were available.

**Cause:**  
Ingredient quantities were stored as strings, causing comparison errors.

**Fix:**  
- Converted ingredient quantities to integers  
- Implemented a real-time `CheckCraftable()` method  
- Updated button interactability dynamically  

---

### 4. UI Buttons Not Responding
**Problem:**  
Settings and menu buttons appeared clickable but did not respond.

**Cause:**  
- Cursor was locked  
- Player camera look was still active  
- UI raycasts were blocked  

**Fix:**  
- Disabled player look when UI panels opened  
- Unlocked and made cursor visible  
- Verified Canvas and Graphic Raycaster setup  

---

### 5. General Debugging & Stability
**Fixes Included:**  
- Added null checks across inventory and crafting systems  
- Used debug logs to trace crafting and UI flow  
- Prevented runtime errors from missing references  

---

## Lessons Learned

- Always validate `Resources.Load()` results  
- Keep UI visuals synchronized with game data  
- ScriptableObjects simplify data-driven systems  
- Small naming mismatches can cause major bugs  

