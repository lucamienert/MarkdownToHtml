using MarkdownToHtml;

var fileInput = FileHandler.ReadFile(@"");

var converter = new Converter();
var html = converter.ConvertToHtml(fileInput);

FileHandler.WriteFile(@"", html);