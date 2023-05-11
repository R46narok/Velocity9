import os
import re

# Define the directory containing the images
image_dir = './'

# Loop over all files in the directory
for filename in os.listdir(image_dir):
    # Check if the file is a JPEG image
    if filename.endswith(".jpg"):
        # Extract the x value from the file name
        match = re.search(r'(\d+)_', filename)
        x = match.group(1)
        
        # Create a directory for this x value if it doesn't already exist
        x_dir = os.path.join(image_dir, x)
        if not os.path.exists(x_dir):
            os.makedirs(x_dir)
        
        # Move the image file into the corresponding directory
        src_path = os.path.join(image_dir, filename)
        dst_path = os.path.join(x_dir, filename)
        os.rename(src_path, dst_path)