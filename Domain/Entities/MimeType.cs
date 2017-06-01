using System.Collections.Generic;

namespace Domain.Entities
{
	public static class MimeType
	{
		private const string DefaultMimeType = "application/octet-stream";
		private static readonly Dictionary<string, string> MimeTypeCollection;
		static MimeType()
		{
			MimeType.MimeTypeCollection = new Dictionary<string, string>();
			MimeType.MimeTypeCollection["ez"] = "application/andrew-inset";
			MimeType.MimeTypeCollection["ez"] = "application/andrew-inset";
			MimeType.MimeTypeCollection["anx"] = "application/annodex";
			MimeType.MimeTypeCollection["atom"] = "application/atom+xml";
			MimeType.MimeTypeCollection["atomcat"] = "application/atomcat+xml";
			MimeType.MimeTypeCollection["atomsrv"] = "application/atomserv+xml";
			MimeType.MimeTypeCollection["lin"] = "application/bbolin";
			MimeType.MimeTypeCollection["cap"] = "application/cap";
			MimeType.MimeTypeCollection["pcap"] = "application/cap";
			MimeType.MimeTypeCollection["cu"] = "application/cu-seeme";
			MimeType.MimeTypeCollection["davmount"] = "application/davmount+xml";
			MimeType.MimeTypeCollection["tsp"] = "application/dsptype";
			MimeType.MimeTypeCollection["es"] = "application/ecmascript";
			MimeType.MimeTypeCollection["spl"] = "application/futuresplash";
			MimeType.MimeTypeCollection["hta"] = "application/hta";
			MimeType.MimeTypeCollection["jar"] = "application/java-archive";
			MimeType.MimeTypeCollection["ser"] = "application/java-serialized-object";
			MimeType.MimeTypeCollection["class"] = "application/java-vm";
			MimeType.MimeTypeCollection["js"] = "application/javascript";
			MimeType.MimeTypeCollection["m3g"] = "application/m3g";
			MimeType.MimeTypeCollection["hqx"] = "application/mac-binhex40";
			MimeType.MimeTypeCollection["cpt"] = "application/mac-compactpro";
			MimeType.MimeTypeCollection["nb"] = "application/mathematica";
			MimeType.MimeTypeCollection["nbp"] = "application/mathematica";
			MimeType.MimeTypeCollection["mdb"] = "application/msaccess";
			MimeType.MimeTypeCollection["doc"] = "application/msword";
			MimeType.MimeTypeCollection["dot"] = "application/msword";
			MimeType.MimeTypeCollection["mxf"] = "application/mxf";
			MimeType.MimeTypeCollection["bin"] = "application/octet-stream";
			MimeType.MimeTypeCollection["oda"] = "application/oda";
			MimeType.MimeTypeCollection["ogx"] = "application/ogg";
			MimeType.MimeTypeCollection["pdf"] = "application/pdf";
			MimeType.MimeTypeCollection["key"] = "application/pgp-keys";
			MimeType.MimeTypeCollection["pgp"] = "application/pgp-signature";
			MimeType.MimeTypeCollection["prf"] = "application/pics-rules";
			MimeType.MimeTypeCollection["ps"] = "application/postscript";
			MimeType.MimeTypeCollection["ai"] = "application/postscript";
			MimeType.MimeTypeCollection["eps"] = "application/postscript";
			MimeType.MimeTypeCollection["epsi"] = "application/postscript";
			MimeType.MimeTypeCollection["epsf"] = "application/postscript";
			MimeType.MimeTypeCollection["eps2"] = "application/postscript";
			MimeType.MimeTypeCollection["eps3"] = "application/postscript";
			MimeType.MimeTypeCollection["rar"] = "application/rar";
			MimeType.MimeTypeCollection["rdf"] = "application/rdf+xml";
			MimeType.MimeTypeCollection["rss"] = "application/rss+xml";
			MimeType.MimeTypeCollection["rtf"] = "application/rtf";
			MimeType.MimeTypeCollection["smi"] = "application/smil";
			MimeType.MimeTypeCollection["smil"] = "application/smil";
			MimeType.MimeTypeCollection["xhtml"] = "application/xhtml+xml";
			MimeType.MimeTypeCollection["xht"] = "application/xhtml+xml";
			MimeType.MimeTypeCollection["xml"] = "application/xml";
			MimeType.MimeTypeCollection["xsl"] = "application/xml";
			MimeType.MimeTypeCollection["xsd"] = "application/xml";
			MimeType.MimeTypeCollection["xspf"] = "application/xspf+xml";
			MimeType.MimeTypeCollection["zip"] = "application/zip";
			MimeType.MimeTypeCollection["apk"] = "application/vnd.android.package-archive";
			MimeType.MimeTypeCollection["cdy"] = "application/vnd.cinderella";
			MimeType.MimeTypeCollection["kml"] = "application/vnd.google-earth.kml+xml";
			MimeType.MimeTypeCollection["kmz"] = "application/vnd.google-earth.kmz";
			MimeType.MimeTypeCollection["xul"] = "application/vnd.mozilla.xul+xml";
			MimeType.MimeTypeCollection["xls"] = "application/vnd.ms-excel";
			MimeType.MimeTypeCollection["xlb"] = "application/vnd.ms-excel";
			MimeType.MimeTypeCollection["xlt"] = "application/vnd.ms-excel";
			MimeType.MimeTypeCollection["cat"] = "application/vnd.ms-pki.seccat";
			MimeType.MimeTypeCollection["stl"] = "application/vnd.ms-pki.stl";
			MimeType.MimeTypeCollection["ppt"] = "application/vnd.ms-powerpoint";
			MimeType.MimeTypeCollection["pps"] = "application/vnd.ms-powerpoint";
			MimeType.MimeTypeCollection["odc"] = "application/vnd.oasis.opendocument.chart";
			MimeType.MimeTypeCollection["odb"] = "application/vnd.oasis.opendocument.database";
			MimeType.MimeTypeCollection["odf"] = "application/vnd.oasis.opendocument.formula";
			MimeType.MimeTypeCollection["odg"] = "application/vnd.oasis.opendocument.graphics";
			MimeType.MimeTypeCollection["otg"] = "application/vnd.oasis.opendocument.graphics-template";
			MimeType.MimeTypeCollection["odi"] = "application/vnd.oasis.opendocument.image";
			MimeType.MimeTypeCollection["odp"] = "application/vnd.oasis.opendocument.presentation";
			MimeType.MimeTypeCollection["otp"] = "application/vnd.oasis.opendocument.presentation-template";
			MimeType.MimeTypeCollection["ods"] = "application/vnd.oasis.opendocument.spreadsheet";
			MimeType.MimeTypeCollection["ots"] = "application/vnd.oasis.opendocument.spreadsheet-template";
			MimeType.MimeTypeCollection["odt"] = "application/vnd.oasis.opendocument.text";
			MimeType.MimeTypeCollection["odm"] = "application/vnd.oasis.opendocument.text-master";
			MimeType.MimeTypeCollection["ott"] = "application/vnd.oasis.opendocument.text-template";
			MimeType.MimeTypeCollection["oth"] = "application/vnd.oasis.opendocument.text-web";
			MimeType.MimeTypeCollection["xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			MimeType.MimeTypeCollection["xltx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
			MimeType.MimeTypeCollection["pptx"] = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
			MimeType.MimeTypeCollection["ppsx"] = "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
			MimeType.MimeTypeCollection["potx"] = "application/vnd.openxmlformats-officedocument.presentationml.template";
			MimeType.MimeTypeCollection["docx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
			MimeType.MimeTypeCollection["dotx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
			MimeType.MimeTypeCollection["cod"] = "application/vnd.rim.cod";
			MimeType.MimeTypeCollection["mmf"] = "application/vnd.smaf";
			MimeType.MimeTypeCollection["sdc"] = "application/vnd.stardivision.calc";
			MimeType.MimeTypeCollection["sds"] = "application/vnd.stardivision.chart";
			MimeType.MimeTypeCollection["sda"] = "application/vnd.stardivision.draw";
			MimeType.MimeTypeCollection["sdd"] = "application/vnd.stardivision.impress";
			MimeType.MimeTypeCollection["sdf"] = "application/vnd.stardivision.math";
			MimeType.MimeTypeCollection["sdw"] = "application/vnd.stardivision.writer";
			MimeType.MimeTypeCollection["sgl"] = "application/vnd.stardivision.writer-global";
			MimeType.MimeTypeCollection["sxc"] = "application/vnd.sun.xml.calc";
			MimeType.MimeTypeCollection["stc"] = "application/vnd.sun.xml.calc.template";
			MimeType.MimeTypeCollection["sxd"] = "application/vnd.sun.xml.draw";
			MimeType.MimeTypeCollection["std"] = "application/vnd.sun.xml.draw.template";
			MimeType.MimeTypeCollection["sxi"] = "application/vnd.sun.xml.impress";
			MimeType.MimeTypeCollection["sti"] = "application/vnd.sun.xml.impress.template";
			MimeType.MimeTypeCollection["sxm"] = "application/vnd.sun.xml.math";
			MimeType.MimeTypeCollection["sxw"] = "application/vnd.sun.xml.writer";
			MimeType.MimeTypeCollection["sxg"] = "application/vnd.sun.xml.writer.global";
			MimeType.MimeTypeCollection["stw"] = "application/vnd.sun.xml.writer.template";
			MimeType.MimeTypeCollection["sis"] = "application/vnd.symbian.install";
			MimeType.MimeTypeCollection["vsd"] = "application/vnd.visio";
			MimeType.MimeTypeCollection["wbxml"] = "application/vnd.wap.wbxml";
			MimeType.MimeTypeCollection["wmlc"] = "application/vnd.wap.wmlc";
			MimeType.MimeTypeCollection["wmlsc"] = "application/vnd.wap.wmlscriptc";
			MimeType.MimeTypeCollection["wpd"] = "application/vnd.wordperfect";
			MimeType.MimeTypeCollection["wp5"] = "application/vnd.wordperfect5.1";
			MimeType.MimeTypeCollection["wk"] = "application/x-123";
			MimeType.MimeTypeCollection["7z"] = "application/x-7z-compressed";
			MimeType.MimeTypeCollection["abw"] = "application/x-abiword";
			MimeType.MimeTypeCollection["dmg"] = "application/x-apple-diskimage";
			MimeType.MimeTypeCollection["bcpio"] = "application/x-bcpio";
			MimeType.MimeTypeCollection["torrent"] = "application/x-bittorrent";
			MimeType.MimeTypeCollection["cab"] = "application/x-cab";
			MimeType.MimeTypeCollection["cbr"] = "application/x-cbr";
			MimeType.MimeTypeCollection["cbz"] = "application/x-cbz";
			MimeType.MimeTypeCollection["cdf"] = "application/x-cdf";
			MimeType.MimeTypeCollection["cda"] = "application/x-cdf";
			MimeType.MimeTypeCollection["vcd"] = "application/x-cdlink";
			MimeType.MimeTypeCollection["pgn"] = "application/x-chess-pgn";
			MimeType.MimeTypeCollection["cpio"] = "application/x-cpio";
			MimeType.MimeTypeCollection["csh"] = "application/x-csh";
			MimeType.MimeTypeCollection["deb"] = "application/x-debian-package";
			MimeType.MimeTypeCollection["udeb"] = "application/x-debian-package";
			MimeType.MimeTypeCollection["dcr"] = "application/x-director";
			MimeType.MimeTypeCollection["dir"] = "application/x-director";
			MimeType.MimeTypeCollection["dxr"] = "application/x-director";
			MimeType.MimeTypeCollection["dms"] = "application/x-dms";
			MimeType.MimeTypeCollection["wad"] = "application/x-doom";
			MimeType.MimeTypeCollection["dvi"] = "application/x-dvi";
			MimeType.MimeTypeCollection["rhtml"] = "application/x-httpd-eruby";
			MimeType.MimeTypeCollection["pfa"] = "application/x-font";
			MimeType.MimeTypeCollection["pfb"] = "application/x-font";
			MimeType.MimeTypeCollection["gsf"] = "application/x-font";
			MimeType.MimeTypeCollection["pcf"] = "application/x-font";
			MimeType.MimeTypeCollection["pcf.Z"] = "application/x-font";
			MimeType.MimeTypeCollection["mm"] = "application/x-freemind";
			MimeType.MimeTypeCollection["gnumeric"] = "application/x-gnumeric";
			MimeType.MimeTypeCollection["sgf"] = "application/x-go-sgf";
			MimeType.MimeTypeCollection["gcf"] = "application/x-graphing-calculator";
			MimeType.MimeTypeCollection["gtar"] = "application/x-gtar";
			MimeType.MimeTypeCollection["tgz"] = "application/x-gtar";
			MimeType.MimeTypeCollection["taz"] = "application/x-gtar";
			MimeType.MimeTypeCollection["hdf"] = "application/x-hdf";
			MimeType.MimeTypeCollection["phtml"] = "application/x-httpd-php";
			MimeType.MimeTypeCollection["pht"] = "application/x-httpd-php";
			MimeType.MimeTypeCollection["php"] = "application/x-httpd-php";
			MimeType.MimeTypeCollection["phps"] = "application/x-httpd-php-source";
			MimeType.MimeTypeCollection["php3"] = "application/x-httpd-php3";
			MimeType.MimeTypeCollection["php3p"] = "application/x-httpd-php3-preprocessed";
			MimeType.MimeTypeCollection["php4"] = "application/x-httpd-php4";
			MimeType.MimeTypeCollection["php5"] = "application/x-httpd-php5";
			MimeType.MimeTypeCollection["ica"] = "application/x-ica";
			MimeType.MimeTypeCollection["info"] = "application/x-info";
			MimeType.MimeTypeCollection["ins"] = "application/x-internet-signup";
			MimeType.MimeTypeCollection["isp"] = "application/x-internet-signup";
			MimeType.MimeTypeCollection["iii"] = "application/x-iphone";
			MimeType.MimeTypeCollection["iso"] = "application/x-iso9660-image";
			MimeType.MimeTypeCollection["jam"] = "application/x-jam";
			MimeType.MimeTypeCollection["jnlp"] = "application/x-java-jnlp-file";
			MimeType.MimeTypeCollection["jmz"] = "application/x-jmol";
			MimeType.MimeTypeCollection["chrt"] = "application/x-kchart";
			MimeType.MimeTypeCollection["kil"] = "application/x-killustrator";
			MimeType.MimeTypeCollection["skp"] = "application/x-koan";
			MimeType.MimeTypeCollection["skd"] = "application/x-koan";
			MimeType.MimeTypeCollection["skt"] = "application/x-koan";
			MimeType.MimeTypeCollection["skm"] = "application/x-koan";
			MimeType.MimeTypeCollection["kpr"] = "application/x-kpresenter";
			MimeType.MimeTypeCollection["kpt"] = "application/x-kpresenter";
			MimeType.MimeTypeCollection["ksp"] = "application/x-kspread";
			MimeType.MimeTypeCollection["kwd"] = "application/x-kword";
			MimeType.MimeTypeCollection["kwt"] = "application/x-kword";
			MimeType.MimeTypeCollection["latex"] = "application/x-latex";
			MimeType.MimeTypeCollection["lha"] = "application/x-lha";
			MimeType.MimeTypeCollection["lyx"] = "application/x-lyx";
			MimeType.MimeTypeCollection["lzh"] = "application/x-lzh";
			MimeType.MimeTypeCollection["lzx"] = "application/x-lzx";
			MimeType.MimeTypeCollection["frm"] = "application/x-maker";
			MimeType.MimeTypeCollection["maker"] = "application/x-maker";
			MimeType.MimeTypeCollection["frame"] = "application/x-maker";
			MimeType.MimeTypeCollection["fm"] = "application/x-maker";
			MimeType.MimeTypeCollection["fb"] = "application/x-maker";
			MimeType.MimeTypeCollection["book"] = "application/x-maker";
			MimeType.MimeTypeCollection["fbdoc"] = "application/x-maker";
			MimeType.MimeTypeCollection["mif"] = "application/x-mif";
			MimeType.MimeTypeCollection["wmd"] = "application/x-ms-wmd";
			MimeType.MimeTypeCollection["wmz"] = "application/x-ms-wmz";
			MimeType.MimeTypeCollection["com"] = "application/x-msdos-program";
			MimeType.MimeTypeCollection["exe"] = "application/x-msdos-program";
			MimeType.MimeTypeCollection["bat"] = "application/x-msdos-program";
			MimeType.MimeTypeCollection["dll"] = "application/x-msdos-program";
			MimeType.MimeTypeCollection["msi"] = "application/x-msi";
			MimeType.MimeTypeCollection["nc"] = "application/x-netcdf";
			MimeType.MimeTypeCollection["pac"] = "application/x-ns-proxy-autoconfig";
			MimeType.MimeTypeCollection["dat"] = "application/x-ns-proxy-autoconfig";
			MimeType.MimeTypeCollection["nwc"] = "application/x-nwc";
			MimeType.MimeTypeCollection["o"] = "application/x-object";
			MimeType.MimeTypeCollection["oza"] = "application/x-oz-application";
			MimeType.MimeTypeCollection["p7r"] = "application/x-pkcs7-certreqresp";
			MimeType.MimeTypeCollection["crl"] = "application/x-pkcs7-crl";
			MimeType.MimeTypeCollection["pyc"] = "application/x-python-code";
			MimeType.MimeTypeCollection["pyo"] = "application/x-python-code";
			MimeType.MimeTypeCollection["qgs"] = "application/x-qgis";
			MimeType.MimeTypeCollection["shp"] = "application/x-qgis";
			MimeType.MimeTypeCollection["shx"] = "application/x-qgis";
			MimeType.MimeTypeCollection["qtl"] = "application/x-quicktimeplayer";
			MimeType.MimeTypeCollection["rpm"] = "application/x-redhat-package-manager";
			MimeType.MimeTypeCollection["rb"] = "application/x-ruby";
			MimeType.MimeTypeCollection["sh"] = "application/x-sh";
			MimeType.MimeTypeCollection["shar"] = "application/x-shar";
			MimeType.MimeTypeCollection["swf"] = "application/x-shockwave-flash";
			MimeType.MimeTypeCollection["swfl"] = "application/x-shockwave-flash";
			MimeType.MimeTypeCollection["scr"] = "application/x-silverlight";
			MimeType.MimeTypeCollection["sit"] = "application/x-stuffit";
			MimeType.MimeTypeCollection["sitx"] = "application/x-stuffit";
			MimeType.MimeTypeCollection["sv4cpio"] = "application/x-sv4cpio";
			MimeType.MimeTypeCollection["sv4crc"] = "application/x-sv4crc";
			MimeType.MimeTypeCollection["tar"] = "application/x-tar";
			MimeType.MimeTypeCollection["tcl"] = "application/x-tcl";
			MimeType.MimeTypeCollection["gf"] = "application/x-tex-gf";
			MimeType.MimeTypeCollection["pk"] = "application/x-tex-pk";
			MimeType.MimeTypeCollection["texinfo"] = "application/x-texinfo";
			MimeType.MimeTypeCollection["texi"] = "application/x-texinfo";
			MimeType.MimeTypeCollection["~"] = "application/x-trash";
			MimeType.MimeTypeCollection["%"] = "application/x-trash";
			MimeType.MimeTypeCollection["bak"] = "application/x-trash";
			MimeType.MimeTypeCollection["old"] = "application/x-trash";
			MimeType.MimeTypeCollection["sik"] = "application/x-trash";
			MimeType.MimeTypeCollection["t"] = "application/x-troff";
			MimeType.MimeTypeCollection["tr"] = "application/x-troff";
			MimeType.MimeTypeCollection["roff"] = "application/x-troff";
			MimeType.MimeTypeCollection["man"] = "application/x-troff-man";
			MimeType.MimeTypeCollection["me"] = "application/x-troff-me";
			MimeType.MimeTypeCollection["ms"] = "application/x-troff-ms";
			MimeType.MimeTypeCollection["ustar"] = "application/x-ustar";
			MimeType.MimeTypeCollection["src"] = "application/x-wais-source";
			MimeType.MimeTypeCollection["wz"] = "application/x-wingz";
			MimeType.MimeTypeCollection["crt"] = "application/x-x509-ca-cert";
			MimeType.MimeTypeCollection["xcf"] = "application/x-xcf";
			MimeType.MimeTypeCollection["fig"] = "application/x-xfig";
			MimeType.MimeTypeCollection["xpi"] = "application/x-xpinstall";
			MimeType.MimeTypeCollection["amr"] = "audio/amr";
			MimeType.MimeTypeCollection["awb"] = "audio/amr-wb";
			MimeType.MimeTypeCollection["axa"] = "audio/annodex";
			MimeType.MimeTypeCollection["au"] = "audio/basic";
			MimeType.MimeTypeCollection["snd"] = "audio/basic";
			MimeType.MimeTypeCollection["flac"] = "audio/flac";
			MimeType.MimeTypeCollection["mid"] = "audio/midi";
			MimeType.MimeTypeCollection["midi"] = "audio/midi";
			MimeType.MimeTypeCollection["kar"] = "audio/midi";
			MimeType.MimeTypeCollection["mpga"] = "audio/mpeg";
			MimeType.MimeTypeCollection["mpega"] = "audio/mpeg";
			MimeType.MimeTypeCollection["mp2"] = "audio/mpeg";
			MimeType.MimeTypeCollection["mp3"] = "audio/mpeg";
			MimeType.MimeTypeCollection["m4a"] = "audio/mpeg";
			MimeType.MimeTypeCollection["m3u"] = "audio/mpegurl";
			MimeType.MimeTypeCollection["oga"] = "audio/ogg";
			MimeType.MimeTypeCollection["ogg"] = "audio/ogg";
			MimeType.MimeTypeCollection["spx"] = "audio/ogg";
			MimeType.MimeTypeCollection["sid"] = "audio/prs.sid";
			MimeType.MimeTypeCollection["aif"] = "audio/x-aiff";
			MimeType.MimeTypeCollection["aiff"] = "audio/x-aiff";
			MimeType.MimeTypeCollection["aifc"] = "audio/x-aiff";
			MimeType.MimeTypeCollection["gsm"] = "audio/x-gsm";
			MimeType.MimeTypeCollection["wma"] = "audio/x-ms-wma";
			MimeType.MimeTypeCollection["wax"] = "audio/x-ms-wax";
			MimeType.MimeTypeCollection["ra"] = "audio/x-pn-realaudio";
			MimeType.MimeTypeCollection["rm"] = "audio/x-pn-realaudio";
			MimeType.MimeTypeCollection["ram"] = "audio/x-pn-realaudio";
			MimeType.MimeTypeCollection["pls"] = "audio/x-scpls";
			MimeType.MimeTypeCollection["sd2"] = "audio/x-sd2";
			MimeType.MimeTypeCollection["wav"] = "audio/x-wav";
			MimeType.MimeTypeCollection["alc"] = "chemical/x-alchemy";
			MimeType.MimeTypeCollection["cac"] = "chemical/x-cache";
			MimeType.MimeTypeCollection["cache"] = "chemical/x-cache";
			MimeType.MimeTypeCollection["csf"] = "chemical/x-cache-csf";
			MimeType.MimeTypeCollection["cbin"] = "chemical/x-cactvs-binary";
			MimeType.MimeTypeCollection["cascii"] = "chemical/x-cactvs-binary";
			MimeType.MimeTypeCollection["ctab"] = "chemical/x-cactvs-binary";
			MimeType.MimeTypeCollection["cdx"] = "chemical/x-cdx";
			MimeType.MimeTypeCollection["cer"] = "chemical/x-cerius";
			MimeType.MimeTypeCollection["c3d"] = "chemical/x-chem3d";
			MimeType.MimeTypeCollection["chm"] = "chemical/x-chemdraw";
			MimeType.MimeTypeCollection["cif"] = "chemical/x-cif";
			MimeType.MimeTypeCollection["cmdf"] = "chemical/x-cmdf";
			MimeType.MimeTypeCollection["cml"] = "chemical/x-cml";
			MimeType.MimeTypeCollection["cpa"] = "chemical/x-compass";
			MimeType.MimeTypeCollection["bsd"] = "chemical/x-crossfire";
			MimeType.MimeTypeCollection["csml"] = "chemical/x-csml";
			MimeType.MimeTypeCollection["csm"] = "chemical/x-csml";
			MimeType.MimeTypeCollection["ctx"] = "chemical/x-ctx";
			MimeType.MimeTypeCollection["cxf"] = "chemical/x-cxf";
			MimeType.MimeTypeCollection["cef"] = "chemical/x-cxf";
			MimeType.MimeTypeCollection["emb"] = "chemical/x-embl-dl-nucleotide";
			MimeType.MimeTypeCollection["embl"] = "chemical/x-embl-dl-nucleotide";
			MimeType.MimeTypeCollection["spc"] = "chemical/x-galactic-spc";
			MimeType.MimeTypeCollection["inp"] = "chemical/x-gamess-input";
			MimeType.MimeTypeCollection["gam"] = "chemical/x-gamess-input";
			MimeType.MimeTypeCollection["gamin"] = "chemical/x-gamess-input";
			MimeType.MimeTypeCollection["fch"] = "chemical/x-gaussian-checkpoint";
			MimeType.MimeTypeCollection["fchk"] = "chemical/x-gaussian-checkpoint";
			MimeType.MimeTypeCollection["cub"] = "chemical/x-gaussian-cube";
			MimeType.MimeTypeCollection["gau"] = "chemical/x-gaussian-input";
			MimeType.MimeTypeCollection["gjc"] = "chemical/x-gaussian-input";
			MimeType.MimeTypeCollection["gjf"] = "chemical/x-gaussian-input";
			MimeType.MimeTypeCollection["gal"] = "chemical/x-gaussian-log";
			MimeType.MimeTypeCollection["gcg"] = "chemical/x-gcg8-sequence";
			MimeType.MimeTypeCollection["gen"] = "chemical/x-genbank";
			MimeType.MimeTypeCollection["hin"] = "chemical/x-hin";
			MimeType.MimeTypeCollection["istr"] = "chemical/x-isostar";
			MimeType.MimeTypeCollection["ist"] = "chemical/x-isostar";
			MimeType.MimeTypeCollection["jdx"] = "chemical/x-jcamp-dx";
			MimeType.MimeTypeCollection["dx"] = "chemical/x-jcamp-dx";
			MimeType.MimeTypeCollection["kin"] = "chemical/x-kinemage";
			MimeType.MimeTypeCollection["mcm"] = "chemical/x-macmolecule";
			MimeType.MimeTypeCollection["mmd"] = "chemical/x-macromodel-input";
			MimeType.MimeTypeCollection["mmod"] = "chemical/x-macromodel-input";
			MimeType.MimeTypeCollection["mol"] = "chemical/x-mdl-molfile";
			MimeType.MimeTypeCollection["rd"] = "chemical/x-mdl-rdfile";
			MimeType.MimeTypeCollection["rxn"] = "chemical/x-mdl-rxnfile";
			MimeType.MimeTypeCollection["sd"] = "chemical/x-mdl-sdfile";
			MimeType.MimeTypeCollection["tgf"] = "chemical/x-mdl-tgf";
			MimeType.MimeTypeCollection["mcif"] = "chemical/x-mmcif";
			MimeType.MimeTypeCollection["mol2"] = "chemical/x-mol2";
			MimeType.MimeTypeCollection["b"] = "chemical/x-molconn-Z";
			MimeType.MimeTypeCollection["gpt"] = "chemical/x-mopac-graph";
			MimeType.MimeTypeCollection["mop"] = "chemical/x-mopac-input";
			MimeType.MimeTypeCollection["mopcrt"] = "chemical/x-mopac-input";
			MimeType.MimeTypeCollection["mpc"] = "chemical/x-mopac-input";
			MimeType.MimeTypeCollection["zmt"] = "chemical/x-mopac-input";
			MimeType.MimeTypeCollection["moo"] = "chemical/x-mopac-out";
			MimeType.MimeTypeCollection["mvb"] = "chemical/x-mopac-vib";
			MimeType.MimeTypeCollection["asn"] = "chemical/x-ncbi-asn1";
			MimeType.MimeTypeCollection["prt"] = "chemical/x-ncbi-asn1-ascii";
			MimeType.MimeTypeCollection["ent"] = "chemical/x-ncbi-asn1-ascii";
			MimeType.MimeTypeCollection["val"] = "chemical/x-ncbi-asn1-binary";
			MimeType.MimeTypeCollection["aso"] = "chemical/x-ncbi-asn1-binary";
			MimeType.MimeTypeCollection["pdb"] = "chemical/x-pdb";
			MimeType.MimeTypeCollection["ros"] = "chemical/x-rosdal";
			MimeType.MimeTypeCollection["sw"] = "chemical/x-swissprot";
			MimeType.MimeTypeCollection["vms"] = "chemical/x-vamas-iso14976";
			MimeType.MimeTypeCollection["vmd"] = "chemical/x-vmd";
			MimeType.MimeTypeCollection["xtel"] = "chemical/x-xtel";
			MimeType.MimeTypeCollection["xyz"] = "chemical/x-xyz";
			MimeType.MimeTypeCollection["gif"] = "image/gif";
			MimeType.MimeTypeCollection["ief"] = "image/ief";
			MimeType.MimeTypeCollection["jpeg"] = "image/jpeg";
			MimeType.MimeTypeCollection["jpg"] = "image/jpeg";
			MimeType.MimeTypeCollection["jpe"] = "image/jpeg";
			MimeType.MimeTypeCollection["pcx"] = "image/pcx";
			MimeType.MimeTypeCollection["png"] = "image/png";
			MimeType.MimeTypeCollection["svg"] = "image/svg+xml";
			MimeType.MimeTypeCollection["svgz"] = "image/svg+xml";
			MimeType.MimeTypeCollection["tiff"] = "image/tiff";
			MimeType.MimeTypeCollection["tif"] = "image/tiff";
			MimeType.MimeTypeCollection["djvu"] = "image/vnd.djvu";
			MimeType.MimeTypeCollection["djv"] = "image/vnd.djvu";
			MimeType.MimeTypeCollection["wbmp"] = "image/vnd.wap.wbmp";
			MimeType.MimeTypeCollection["cr2"] = "image/x-canon-cr2";
			MimeType.MimeTypeCollection["crw"] = "image/x-canon-crw";
			MimeType.MimeTypeCollection["ras"] = "image/x-cmu-raster";
			MimeType.MimeTypeCollection["cdr"] = "image/x-coreldraw";
			MimeType.MimeTypeCollection["pat"] = "image/x-coreldrawpattern";
			MimeType.MimeTypeCollection["cdt"] = "image/x-coreldrawtemplate";
			MimeType.MimeTypeCollection["erf"] = "image/x-epson-erf";
			MimeType.MimeTypeCollection["ico"] = "image/x-icon";
			MimeType.MimeTypeCollection["art"] = "image/x-jg";
			MimeType.MimeTypeCollection["jng"] = "image/x-jng";
			MimeType.MimeTypeCollection["bmp"] = "image/x-ms-bmp";
			MimeType.MimeTypeCollection["nef"] = "image/x-nikon-nef";
			MimeType.MimeTypeCollection["orf"] = "image/x-olympus-orf";
			MimeType.MimeTypeCollection["psd"] = "image/x-photoshop";
			MimeType.MimeTypeCollection["pnm"] = "image/x-portable-anymap";
			MimeType.MimeTypeCollection["pbm"] = "image/x-portable-bitmap";
			MimeType.MimeTypeCollection["pgm"] = "image/x-portable-graymap";
			MimeType.MimeTypeCollection["ppm"] = "image/x-portable-pixmap";
			MimeType.MimeTypeCollection["rgb"] = "image/x-rgb";
			MimeType.MimeTypeCollection["xbm"] = "image/x-xbitmap";
			MimeType.MimeTypeCollection["xpm"] = "image/x-xpixmap";
			MimeType.MimeTypeCollection["xwd"] = "image/x-xwindowdump";
			MimeType.MimeTypeCollection["eml"] = "message/rfc822";
			MimeType.MimeTypeCollection["igs"] = "model/iges";
			MimeType.MimeTypeCollection["iges"] = "model/iges";
			MimeType.MimeTypeCollection["msh"] = "model/mesh";
			MimeType.MimeTypeCollection["mesh"] = "model/mesh";
			MimeType.MimeTypeCollection["silo"] = "model/mesh";
			MimeType.MimeTypeCollection["wrl"] = "model/vrml";
			MimeType.MimeTypeCollection["vrml"] = "model/vrml";
			MimeType.MimeTypeCollection["x3dv"] = "model/x3d+vrml";
			MimeType.MimeTypeCollection["x3d"] = "model/x3d+xml";
			MimeType.MimeTypeCollection["x3db"] = "model/x3d+binary";
			MimeType.MimeTypeCollection["manifest"] = "text/cache-manifest";
			MimeType.MimeTypeCollection["ics"] = "text/calendar";
			MimeType.MimeTypeCollection["icz"] = "text/calendar";
			MimeType.MimeTypeCollection["css"] = "text/css";
			MimeType.MimeTypeCollection["csv"] = "text/csv";
			MimeType.MimeTypeCollection["323"] = "text/h323";
			MimeType.MimeTypeCollection["html"] = "text/html";
			MimeType.MimeTypeCollection["htm"] = "text/html";
			MimeType.MimeTypeCollection["shtml"] = "text/html";
			MimeType.MimeTypeCollection["uls"] = "text/iuls";
			MimeType.MimeTypeCollection["mml"] = "text/mathml";
			MimeType.MimeTypeCollection["asc"] = "text/plain";
			MimeType.MimeTypeCollection["txt"] = "text/plain";
			MimeType.MimeTypeCollection["text"] = "text/plain";
			MimeType.MimeTypeCollection["pot"] = "text/plain";
			MimeType.MimeTypeCollection["brf"] = "text/plain";
			MimeType.MimeTypeCollection["rtx"] = "text/richtext";
			MimeType.MimeTypeCollection["sct"] = "text/scriptlet";
			MimeType.MimeTypeCollection["wsc"] = "text/scriptlet";
			MimeType.MimeTypeCollection["tm"] = "text/texmacs";
			MimeType.MimeTypeCollection["ts"] = "text/texmacs";
			MimeType.MimeTypeCollection["tsv"] = "text/tab-separated-values";
			MimeType.MimeTypeCollection["jad"] = "text/vnd.sun.j2me.app-descriptor";
			MimeType.MimeTypeCollection["wml"] = "text/vnd.wap.wml";
			MimeType.MimeTypeCollection["wmls"] = "text/vnd.wap.wmlscript";
			MimeType.MimeTypeCollection["bib"] = "text/x-bibtex";
			MimeType.MimeTypeCollection["boo"] = "text/x-boo";
			MimeType.MimeTypeCollection["h++"] = "text/x-c++hdr";
			MimeType.MimeTypeCollection["hpp"] = "text/x-c++hdr";
			MimeType.MimeTypeCollection["hxx"] = "text/x-c++hdr";
			MimeType.MimeTypeCollection["hh"] = "text/x-c++hdr";
			MimeType.MimeTypeCollection["c++"] = "text/x-c++src";
			MimeType.MimeTypeCollection["cpp"] = "text/x-c++src";
			MimeType.MimeTypeCollection["cxx"] = "text/x-c++src";
			MimeType.MimeTypeCollection["cc"] = "text/x-c++src";
			MimeType.MimeTypeCollection["h"] = "text/x-chdr";
			MimeType.MimeTypeCollection["htc"] = "text/x-component";
			MimeType.MimeTypeCollection["c"] = "text/x-csrc";
			MimeType.MimeTypeCollection["d"] = "text/x-dsrc";
			MimeType.MimeTypeCollection["diff"] = "text/x-diff";
			MimeType.MimeTypeCollection["patch"] = "text/x-diff";
			MimeType.MimeTypeCollection["hs"] = "text/x-haskell";
			MimeType.MimeTypeCollection["java"] = "text/x-java";
			MimeType.MimeTypeCollection["lhs"] = "text/x-literate-haskell";
			MimeType.MimeTypeCollection["moc"] = "text/x-moc";
			MimeType.MimeTypeCollection["p"] = "text/x-pascal";
			MimeType.MimeTypeCollection["pas"] = "text/x-pascal";
			MimeType.MimeTypeCollection["gcd"] = "text/x-pcs-gcd";
			MimeType.MimeTypeCollection["pl"] = "text/x-perl";
			MimeType.MimeTypeCollection["pm"] = "text/x-perl";
			MimeType.MimeTypeCollection["py"] = "text/x-python";
			MimeType.MimeTypeCollection["scala"] = "text/x-scala";
			MimeType.MimeTypeCollection["etx"] = "text/x-setext";
			MimeType.MimeTypeCollection["tk"] = "text/x-tcl";
			MimeType.MimeTypeCollection["tex"] = "text/x-tex";
			MimeType.MimeTypeCollection["ltx"] = "text/x-tex";
			MimeType.MimeTypeCollection["sty"] = "text/x-tex";
			MimeType.MimeTypeCollection["cls"] = "text/x-tex";
			MimeType.MimeTypeCollection["vcs"] = "text/x-vcalendar";
			MimeType.MimeTypeCollection["vcf"] = "text/x-vcard";
			MimeType.MimeTypeCollection["3gp"] = "video/3gpp";
			MimeType.MimeTypeCollection["axv"] = "video/annodex";
			MimeType.MimeTypeCollection["dl"] = "video/dl";
			MimeType.MimeTypeCollection["dif"] = "video/dv";
			MimeType.MimeTypeCollection["dv"] = "video/dv";
			MimeType.MimeTypeCollection["fli"] = "video/fli";
			MimeType.MimeTypeCollection["gl"] = "video/gl";
			MimeType.MimeTypeCollection["mpeg"] = "video/mpeg";
			MimeType.MimeTypeCollection["mpg"] = "video/mpeg";
			MimeType.MimeTypeCollection["mpe"] = "video/mpeg";
			MimeType.MimeTypeCollection["mp4"] = "video/mp4";
			MimeType.MimeTypeCollection["qt"] = "video/quicktime";
			MimeType.MimeTypeCollection["mov"] = "video/quicktime";
			MimeType.MimeTypeCollection["ogv"] = "video/ogg";
			MimeType.MimeTypeCollection["mxu"] = "video/vnd.mpegurl";
			MimeType.MimeTypeCollection["flv"] = "video/x-flv";
			MimeType.MimeTypeCollection["lsf"] = "video/x-la-asf";
			MimeType.MimeTypeCollection["lsx"] = "video/x-la-asf";
			MimeType.MimeTypeCollection["mng"] = "video/x-mng";
			MimeType.MimeTypeCollection["asf"] = "video/x-ms-asf";
			MimeType.MimeTypeCollection["asx"] = "video/x-ms-asf";
			MimeType.MimeTypeCollection["wm"] = "video/x-ms-wm";
			MimeType.MimeTypeCollection["wmv"] = "video/x-ms-wmv";
			MimeType.MimeTypeCollection["wmx"] = "video/x-ms-wmx";
			MimeType.MimeTypeCollection["wvx"] = "video/x-ms-wvx";
			MimeType.MimeTypeCollection["avi"] = "video/x-msvideo";
			MimeType.MimeTypeCollection["movie"] = "video/x-sgi-movie";
			MimeType.MimeTypeCollection["mpv"] = "video/x-matroska";
			MimeType.MimeTypeCollection["mkv"] = "video/x-matroska";
			MimeType.MimeTypeCollection["ice"] = "x-conference/x-cooltalk";
			MimeType.MimeTypeCollection["sisx"] = "x-epoc/x-sisx-app";
			MimeType.MimeTypeCollection["vrm"] = "x-world/x-vrml";
		}
		public static string Get(string extension)
		{
			if (extension.StartsWith("."))
			{
				extension = extension.Remove(0, 1);
			}
			string result;
			if (MimeType.MimeTypeCollection.ContainsKey(extension))
			{
				result = MimeType.MimeTypeCollection[extension];
			}
			else
			{
				result = "application/octet-stream";
			}
			return result;
		}
	}
}