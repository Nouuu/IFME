#!/bin/sh
ORIDIR="$(dirname $(readlink -f $0))"
CompileMode="Debug"
BUILDDIR="build"

cd "$ORIDIR"

echo "Currently you need already compiled version,"
echo "compiling on Linux for now quite possible, so,"
echo "this script will make it standalone program"
echo "without Mono required to install"
echo " "
echo " "
echo " "
echo "Before you start:"
echo "      1. Mono 4.0+ <http://www.mono-project.com/download/#download-lin>"
echo "      2. sudo apt-get install p7zip-full mediainfo"
echo " "
echo " "
echo " "

while true; do
    read -p "Do you wish to download all required item before compile? (Y/n): " yn
    case $yn in
        [Yy]* ) sh prerequisite/deploy.sh; sh references/download.sh; break;;
		[Nn]* ) break;;
        * ) echo "Please answer yes or no.";;
    esac
done

echo "Return to original path"
cd "$ORIDIR"

echo "Remove windows builds"
rm -rf "$BUILDDIR"

echo "Make new folder"
mkdir "$BUILDDIR"

echo "Building..."
xbuild /nologo /verbosity:normal ifme.sln /target:Build /property:Configuration=$CompileMode

echo "Copying stuff"
mkdir "$BUILDDIR/benchmark"
mkdir "$BUILDDIR/extension"
cp -r "ifme/lang" "$BUILDDIR/"
cp -r "ifme/profile" "$BUILDDIR/"
cp -r "ifme/sounds" "$BUILDDIR/"
cp -r "ifme/addons_linux32.repo" "$BUILDDIR/"
cp -r "ifme/addons_linux64.repo" "$BUILDDIR/"
cp -r "ifme/addons_windows32.repo" "$BUILDDIR/"
cp -r "ifme/addons_windows32.repo" "$BUILDDIR/"
cp -r "ifme/avisynthsource.code" "$BUILDDIR/"
cp -r "ifme/format.ini" "$BUILDDIR/"
cp -r "ifme/iso.code" "$BUILDDIR/"
cp -r "sources/metauser.if" "$BUILDDIR/"
cp -r "changelog.txt" "$BUILDDIR/"
cp -r "license.txt" "$BUILDDIR/"
cp -r "patents.txt" "$BUILDDIR/"

cp "/usr/lib/p7zip/7za" "$BUILDDIR/"
cp -a "/usr/lib/x86_64-linux-gnu/libmediainfo.so.0.0.0" "$BUILDDIR/"
cp -a "/usr/lib/x86_64-linux-gnu/libmediainfo.so.0" "$BUILDDIR/"
cp -a "/usr/lib/x86_64-linux-gnu/libzen.so.0.0.0" "$BUILDDIR/"
cp -a "/usr/lib/x86_64-linux-gnu/libzen.so.0" "$BUILDDIR/"
cp -a "/usr/lib/x86_64-linux-gnu/libtinyxml2.so.0.0.0" "$BUILDDIR/"

echo "Copying compiled"
cp "ifme/bin/$CompileMode/ifme.exe" "$BUILDDIR/"
cp "ifme/bin/$CompileMode/INIFileParser.dll" "$BUILDDIR/"
cp "ifme/bin/$CompileMode/MediaInfoDotNet.dll" "$BUILDDIR/"
cp "sources/MediaInfoDotNet.dll.config" "$BUILDDIR/"

echo "Copying plugins"
cp -r "prerequisite/linux/64bit/plugins" "$BUILDDIR/"

echo "Copying extension"
cp -r "prerequisite/allos/extension" "$BUILDDIR/"

echo "Please Wait..."
sleep 3

cd $BUILDDIR
mkbundle --deps --static -o ifme-bin ifme.exe INIFileParser.dll MediaInfoDotNet.dll

gcc "../sources/ifme-gnome.c" -o "ifme-gnome"
gcc "../sources/ifme-xterm.c" -o "ifme-xterm"

cp "../source/ifme.sh" "ifme"
chmod +x "$BUILDDIR/ifme"

echo "Remove bytecode"
rm -f "ifme.exe"
rm -f "INIFileParser.dll"
rm -f "MediaInfoDotNet.dll"
rm -f "MediaInfoDotNet.dll.config"

cd "$ORIDIR"

echo "Fix directory permission"
find "$ORIDIR/$BUILDDIR" -type d -exec chmod 775 {} +

echo "Packaging..."
mv $BUILDDIR ifme5
tar -cvJf ifme5-x64_linux.tar.xz ifme5
mv ifme5 $BUILDDIR

echo "Done!"
sleep 3