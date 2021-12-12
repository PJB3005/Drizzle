import os
import re
import json
from collections import defaultdict
from typing import Dict, DefaultDict

LOG_FILE_NAME_RE = re.compile(r"\d+_build \((.+?)\).txt")
LOG_CHECKSUM_RE = re.compile(r"checksum\s+(.+?)\s+cam\s+(\d+):\s+([A-F0-9]+)", re.IGNORECASE)

checksums_parsed: Dict[str, DefaultDict[str, Dict[str, str]]] = {}

for f in os.listdir("Logs"):
    if not (m := LOG_FILE_NAME_RE.match(f)):
        continue

    this_file: DefaultDict[str, Dict[str, str]] = defaultdict(dict)
    checksums_parsed[m.group(1)] = this_file

    print(f"Opening file {f}")
    with open(os.path.join("Logs", f), "r") as log:
        while line := log.readline():
            if not (m := LOG_CHECKSUM_RE.search(line)):
                continue

            room_name = m.group(1)
            room_cam = m.group(2)
            room_checksum = m.group(3)

            print(f"  {room_name}#{room_cam}:\t{room_checksum}")
            this_file[room_name][room_cam] = room_checksum

    dataDir = os.path.join("..", "Data", "LevelEditorProjects")
    for name, data in checksums_parsed.items():
        if name.startswith("World"):
            name = os.path.join("World", name[5:])

        checksums_manifest = os.path.join(dataDir, name, "checksums.json")

        with open(checksums_manifest, "w") as manifest:
            json.dump(data, manifest, sort_keys=True, indent=4)

