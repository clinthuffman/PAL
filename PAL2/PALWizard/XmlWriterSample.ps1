# this is where the document will be saved:
$Path = "$env:temp\inventory.xml"
 
# get an XMLTextWriter to create the XML
$XmlWriter = New-Object System.XMl.XmlTextWriter($Path,$Null)
 
# choose a pretty formatting:
$xmlWriter.Formatting = 'Indented'
$xmlWriter.Indentation = 1
$XmlWriter.IndentChar = "`t"
 
# write the header
$xmlWriter.WriteStartDocument()
 
# set XSL statements
$xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='style.xsl'")
 
# create root element "machines" and add some attributes to it
$XmlWriter.WriteComment('List of machines')
$xmlWriter.WriteStartElement('Machines')
$XmlWriter.WriteAttributeString('current', $true)
$XmlWriter.WriteAttributeString('manager', 'Tobias')
 
# add a couple of random entries
for($x=1; $x -le 10; $x++)
{
    $server = 'Server{0:0000}' -f $x
    $ip = '{0}.{1}.{2}.{3}' -f  (0..256 | Get-Random -Count 4)
 
    $guid = [System.GUID]::NewGuid().ToString()
 
    # each data set is called "machine", add a random attribute to it:
    $XmlWriter.WriteComment("$x. machine details")
    $xmlWriter.WriteStartElement('Machine')
    $XmlWriter.WriteAttributeString('test', (Get-Random))
 
    # add three pieces of information:
    $xmlWriter.WriteElementString('Name',$server)
    $xmlWriter.WriteElementString('IP',$ip)
    $xmlWriter.WriteElementString('GUID',$guid)
 
    # add a node with attributes and content:
    $XmlWriter.WriteStartElement('Information')
    $XmlWriter.WriteAttributeString('info1', 'some info')
    $XmlWriter.WriteAttributeString('info2', 'more info')
    $XmlWriter.WriteRaw('RawContent')
    $xmlWriter.WriteEndElement()
 
    # add a node with CDATA section:
    $XmlWriter.WriteStartElement('CodeSegment')
    $XmlWriter.WriteAttributeString('info3', 'another attribute')
    $XmlWriter.WriteCData('this is untouched code and can contain special characters /\@<>')
    $xmlWriter.WriteEndElement()
 
    # close the "machine" node:
    $xmlWriter.WriteEndElement()
}
 
# close the "machines" node:
$xmlWriter.WriteEndElement()
 
# finalize the document:
$xmlWriter.WriteEndDocument()
$xmlWriter.Flush()
$xmlWriter.Close()
 
notepad $path
