# Notes about Lingo/Director

This just contains various bits of detail I figured I should write down about lingo/director.

## Images

* Images are 0-indexed, so 0,0 is the top left corner. Stuff like `copyPixels()` rectangles all uses sane indexing.
* The bitmap view in Director doesn't put 0,0 at the top left corner, you have to scroll to the top left to view the whole image.
* **image depths:**
    * 1/2/4/8 are all indexed color.
    * 16 is RGBA5551 or similar.
    * 32 is RGBA32 with alpha channel only one bit still.
* out-of-bounds reads with `copyPixels()` will basically be treated as if the out of bounds region is empty.
* Copying to a 1-bit image seems somewhat intelligent about whether to pick white or black based on input color. Primary colors (like pure red) seem to be black, but secondaries like yellow are white. Probably just a specific case of palettization, but still.