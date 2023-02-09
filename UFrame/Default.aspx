<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="iPanelTest" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UFrame Test Form</title>
    
    <script src="Javascripts/jquery-1.2.3.js" type="text/javascript"></script>
    <script src="Javascripts/htmlparser.js" type="text/javascript"></script>
    <script src="Javascripts/UFrame.js" type="text/javascript"></script>
    
    <style type="text/css">
        body {
        	background-color: #666600;
	        color: #FFFFFF;
	        font-family: Arial, Helvetica, sans-serif;
	    }
        
        div.UFrame { width: 300px; height: 300px; overflow: scroll; border: 1px solid black; background-color: white; color:black; padding: 5px }
        div.box { float: left; width: 310px; margin: 5px}
        fieldset.testarea { background-color: #4F4F00; border:none; width: 1000px; margin-bottom: 20px; padding: 10px; }
        fieldset.testarea em { color:lightgreen; }
        fieldset.testarea a { color:lightblue; }        
        fieldset.testarea h3 { font-size: 1em; height: 2.5em; }
        div.no { font-size: 4em; font-family: Georgia,"Times New Roman",Times,serif; }
        
        p.description
        {
            color: #FFFF99;
            font-size: 0.7em;
            font-weight: bold;
            height: 7em;
            
        }
        p.description em { color: lightgreen; font-size: larger }
    </style>
</head>
<body >
    <form id="UFrameTest" runat="server">
	<h1>UFrame = UpdatePanel + IFRAME</h1>
    <h2>Goodness of UpdatePanel and browser IFRAME combined</h2>
        
    <fieldset class="testarea">
        <div class="box">
            <div class="no">1.</div>
            <h3>Load any page inside a DIV</h3>
            <p class="description">An ASP.NET Page named SomePage.aspx is loaded inside a DIV. You can click on the buttons and see the page postbacks within the DIV just like an IFRAME. You can see the <em>DataGrid</em> on SomePage.aspx works just fine.</p>
            <div class="UFrame" id="UFrame1" src="Pages/SomePage.aspx?ID=UFrame1" >
                <p>This should get replaced with content from Somepage.aspx</p>
            </div>
            <p class="description">THIS IS NOT AN IFRAME<br />
            &lt;div class="UFrame" id="UFrame1" src="Pages/SomePage.aspx?ID=UFrame1" &gt;&lt;/div&gt;</p>
        </div>
        
        <div class="box">
            <div class="no">2.</div>
            <h3>Load pages like widgets</h3>
            <p class="description">Another instance of SomePage.aspx is loaded inside a DIV. This shows same page can be loaded many times within the same parent page. You will see the <em>DataGrid</em> within this instance also works fine without conflicting with the previous one</p>
            <div class="UFrame" id="UFrame2" src="Pages/SomePage.aspx?ID=UFrame2" progressTemplate="<h1>Loading...</h1>">
                <p>This should get replaced with content from Somepage.aspx</p>
            </div>
            <p class="description">THIS IS NOT AN IFRAME<br />
            &lt;div class="UFrame" id="UFrame2" src="Pages/SomePage.aspx?ID=UFrame2" progressTemplate="&lt;h1&gt;Loading...&lt;/h1&gt;"&gt;<br />
            &lt;/div&gt;</p>
        </div>
        
        <div class="box">
            <div class="no">3.</div>
            <h3>Load different pages inside same document</h3>
            <p class="description">Following loads Flickr photos and pages through them using regular postback. It shows <em>ViewState</em> and standard ASP.NET <em>Postback Events</em> works fine</p>
            <div class="UFrame" id="TestPage" src="Pages/AnotherPage.aspx" progressTemplate="&lt;p&gt;Loading...&lt;/p&gt;">
                <p>Loading Flickr photos...</p>
            </div>
            <p class="description">THIS IS NOT AN IFRAME<br />
            &lt;div class="UFrame" id="TestPage" src="Pages/AnotherPage.aspx" &gt;<br />
            &lt;/div&gt;
            </p>
        </div>
    </fieldset>
    
    <fieldset class="testarea">    
        <h3>What is UFrame?</h3>
        <p><em>UFrame</em> is like an IFRAME that can load and host a page (ASP.NET, PHP or regular html) inside a DIV. 
        However, unlike IFRAME which loads the content inside a frame that has no relation with the main document, 
        <em>UFrame</em> loads the content within the same document. Thus all the Javascripts, CSS on the main document
        applies to the loaded content. It's just like UpdatePanel with IFRAME's "src" attribute.</p>
        <p>The above <em>UFrame</em> are declared like this:</p>
        <pre>
&lt;div id="UFrame1" src="SomePage.aspx" &gt;
    &lt;p&gt;This should get replaced with content from Somepage.aspx&lt;/p&gt;
&lt;/div&gt;</pre>
        <p>The features of <em>UFrame</em> are:</p>
        <ul>
        <li>Load any URL inside a DIV. It can be a php, asp.net, jsp or regular HTML page.</li>
        <li>Just like IFRAME, you can set "src" property of DIVs and they are converted to <em>UFrame</em>s.</li>
        <li>Unlike IFRAME, it loads the content within the main document. So, main document's CSS and Javascripts are available to the loaded content.</li>
        <li>It allows you to build parts of a page as multiple fully independent pages, instead of controls.</li>
        <li>Each page is built as standalone page. You can build, test and debug each page indepedently and them put them together using UFrame.</li>
        <li>It loads both inline and external scripts from loaded page. You can also produce different scripts during postback.</li>
        <li>All external scripts are loaded before the body content is set. And all inline scripts are executed when both external scripts and body has been loaded.</li>
        <li>It loads both inline and external CSS</li>
        
        </ul>
        
        <h2>Download Code</h2>
        <p>Download latest source code of UFrame from CodePlex: <a href="http://www.codeplex.com/UFrame">www.codeplex.com/UFrame</a></p>
        
    </fieldset>
    
    </form>
</body>
</html>
