﻿using System.Xml;

namespace PopStudio.Package.Pak
{
    /// <summary>
    /// SexyApp Framework Pak 
    /// </summary>
    internal class Pak
    {
        public static Dictionary<string, CompressFlags> compressDictionary = new Dictionary<string, CompressFlags> { { ".ptx", CompressFlags.ZLIB }, { ".compiled", CompressFlags.ZLIB }, { ".txt", CompressFlags.ZLIB }, { ".xml", CompressFlags.ZLIB }, { ".reanim", CompressFlags.ZLIB } };
        public static CompressFlags defaultcompressMethod = CompressFlags.STORE;

        public static void Pack(string inFolder, string outFile)
        {
            if (!Directory.Exists(inFolder))
            {
                throw new Exception(string.Format(Str.Obj.FolderNotFound, inFolder));
            }
            Dir.NewDir(outFile, false);
            PakInfo pak = new PakInfo
            {
                fileInfoLibrary = new List<FileInfo>()
            };
            //read some parameter
            string xmlpath = inFolder + Const.PATHSEPARATOR + "popstudioinfo" + Const.PATHSEPARATOR + "packinfo.xml";
            if (File.Exists(xmlpath))
            {
                string xmldata = File.ReadAllText(xmlpath);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmldata);
                var root = xml.SelectSingleNode("/PackInfo");
                if (root == null) return;
                var childlist = root.ChildNodes;
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "PCVersion")
                    {
                        pak.pc = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "TVVersion")
                    {
                        if (Convert.ToBoolean(child.InnerText)) throw new Exception();
                    }
                    else if (child.Name == "WindowsPathSeparate")
                    {
                        pak.win = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "Xbox360PtxAlign")
                    {
                        pak.x360 = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "XmemCompress")
                    {
                        pak.xmem = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "ZlibCompress")
                    {
                        pak.compress = Convert.ToBoolean(child.InnerText);
                    }
                }
            }
            else
            {
                pak.pc = true;
                pak.win = true;
                pak.x360 = false;
                pak.xmem = false;
                pak.compress = false;
            }
            string[] a = Dir.GetFiles(inFolder);
            int temp = inFolder.Length + 1;
            bool firstx360align = false;
            using (BinaryStream bs_files = new BinaryStream())
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == xmlpath) continue;
                    if (i != 0 && (!pak.pc))
                    {
                        if (pak.x360 && Path.GetExtension(a[i]).ToLower() == ".ptx")
                        {
                            PakInfo.Fill0x1000(bs_files);
                        }
                        else
                        {
                            PakInfo.Fill(bs_files);
                        }
                    }
                    if (i == 0 && (!pak.pc))
                    {
                        firstx360align = pak.x360 && (Path.GetExtension(a[i]).ToLower() == ".ptx");
                    }
                    FileInfo info = new FileInfo();
                    using (BinaryStream bs_thisfile = new BinaryStream(a[i], FileMode.Open))
                    {
                        if (pak.compress == true)
                        {
                            CompressFlags method = GetCompressMethod(a[i]);
                            switch (method)
                            {
                                case CompressFlags.STORE:
                                    bs_thisfile.CopyTo(bs_files);
                                    info.zsize = (int)bs_thisfile.Length;
                                    break;
                                case CompressFlags.ZLIB:
                                    using (BinaryStream bs3 = new BinaryStream())
                                    {
                                        using (ZLibStream zLibStream = new ZLibStream(bs3, CompressionMode.Compress, true))
                                        {
                                            bs_thisfile.CopyTo(zLibStream);
                                        }
                                        bs3.Position = 0;
                                        bs3.CopyTo(bs_files);
                                        info.size = (int)bs_thisfile.Length;
                                        info.zsize = (int)bs3.Length;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            bs_thisfile.CopyTo(bs_files);
                            info.zsize = (int)bs_thisfile.Length;
                        }
                    }
                    if (pak.win)
                    {
                        info.fileName = Dir.FormatWindowsPath(a[i][temp..]);
                    }
                    else
                    {
                        info.fileName = Dir.FormatLinuxPath(a[i][temp..]);
                    }
                    pak.fileInfoLibrary.Add(info);
                }
                if (pak.pc)
                {
                    using (BinaryStream bs_head = new BinaryStream())
                    {
                        pak.Write(bs_head);
                        int l1 = (int)bs_head.Length;
                        int l2 = (int)bs_files.Length;
                        bs_head.Position = 0;
                        bs_files.Position = 0;
                        using (BinaryStream bs = new BinaryStream(outFile, FileMode.Create))
                        {
                            for (int i = 0; i < l1; i++)
                            {
                                bs.WriteUInt8((byte)(bs_head.ReadUInt8() ^ 0xF7));
                            }
                            for (int i = 0; i < l2; i++)
                            {
                                bs.WriteUInt8((byte)(bs_files.ReadUInt8() ^ 0xF7));
                            }
                        }
                    }
                    
                }
                else if (pak.xmem)
                {
                    throw new Exception(Str.Obj.XmemCompressInvalid);
                }
                else
                {
                    using (BinaryStream bs = new BinaryStream(outFile, FileMode.Create))
                    {
                        pak.Write(bs);
                        if (pak.x360 && firstx360align)
                        {
                            PakInfo.Fill0x1000(bs);
                        }
                        else
                        {
                            PakInfo.Fill(bs);
                        }
                        bs_files.Position = 0;
                        bs_files.CopyTo(bs);
                    }
                }
            }
        }

        public static CompressFlags GetCompressMethod(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            if (compressDictionary.ContainsKey(extension))
            {
                return compressDictionary[extension];
            }
            return defaultcompressMethod;
        }

        public static void Unpack(string inFile, string outFolder, bool changeimage = false, bool delete = false)
        {
            if (!File.Exists(inFile))
            {
                throw new Exception(string.Format(Str.Obj.FileNotFound, inFile));
            }
            Dir.NewDir(outFolder);
            outFolder += Const.PATHSEPARATOR;
            string tempName;
            using (BinaryStream bs = new BinaryStream())
            {
                PakInfo pak = new PakInfo();
                using (BinaryStream bs_origin = BinaryStream.Open(inFile))
                {
                    int magic = bs_origin.PeekInt32();
                    if (magic == 1295498551) //37 BD 37 4D
                    {
                        //pak PC xor 0xf7 encrypt
                        pak.pc = true;
                        //pak.compress = false;
                        int l = (int)bs_origin.Length;
                        for (int i = 0; i < l; i++)
                        {
                            bs.WriteUInt8((byte)(bs_origin.ReadUInt8() ^ 0xF7));
                        }
                    }
                    else if (magic == -1161803072) //C0 4A C0 BA
                    {
                        //pak game console
                        bs.WriteBytes(bs_origin.ReadBytes((int)bs_origin.Length));
                    }
                    else if (magic == -317524721) //0F F5 12 ED
                    {
                        //xmem compress file in xbox360
                        //Invalid
                        pak.xmem = true;
                        pak.compress = false;
                        throw new Exception(Str.Obj.XmemCompressInvalid);
                    }
                }
                bs.Position = 0;
                pak.Read(bs);
                if (pak.fileInfoLibrary == null) return;
                for (int i = 0; i < pak.fileInfoLibrary.Count; i++)
                {
                    if (!pak.pc) pak.x360 |= PakInfo.Jump(bs);
                    tempName = Dir.FormatPath(outFolder + pak.fileInfoLibrary[i].fileName);
                    Dir.NewDir(tempName, false);
                    if (pak.fileInfoLibrary[i].size != 0)
                    {
                        using (BinaryStream bs2 = new BinaryStream())
                        {
                            bs2.WriteBytes(bs.ReadBytes(pak.fileInfoLibrary[i].zsize));
                            bs2.Position = 0;
                            using (ZLibStream zLibStream = new ZLibStream(bs2, CompressionMode.Decompress))
                            {
                                using (BinaryStream bs3 = new BinaryStream(tempName, FileMode.Create))
                                {
                                    zLibStream.CopyTo(bs3);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (BinaryStream bs2 = new BinaryStream(tempName, FileMode.Create))
                        {
                            bs2.WriteBytes(bs.ReadBytes(pak.fileInfoLibrary[i].zsize));
                        }
                    }
                    if (changeimage && Path.GetExtension(tempName).ToLower() == ".ptx")
                    {
                        if (pak.x360)
                        {
                            Image.PtxXbox360.Ptx.Decode(tempName, Path.ChangeExtension(tempName, ".png"));
                        }
                        if (delete) File.Delete(tempName);
                    }
                }
                string lst = outFolder + "popstudioinfo";
                Dir.NewDir(lst);
                using (StreamWriter sw = new StreamWriter(lst + Const.PATHSEPARATOR + "packinfo.xml", false))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<!-- DO NOT EDIT THIS FILE. This file is generated by PopStudio. (unless you want to change the way for pack) -->");
                    sw.WriteLine("<PackInfo version=\"1\">");
                    sw.WriteLine("    <PCVersion>" + pak.pc + "</PCVersion>");
                    sw.WriteLine("    <TVVersion>False</TVVersion>");
                    sw.WriteLine("    <WindowsPathSeparate>" + pak.win + "</WindowsPathSeparate>");
                    sw.WriteLine("    <Xbox360PtxAlign>" + pak.x360 + "</Xbox360PtxAlign>");
                    sw.WriteLine("    <XmemCompress>" + pak.xmem + "</XmemCompress>");
                    sw.WriteLine("    <ZlibCompress>" + (pak.compress == true) + "</ZlibCompress>");
                    sw.WriteLine("</PackInfo>");
                }
            }
        }
    }
}
