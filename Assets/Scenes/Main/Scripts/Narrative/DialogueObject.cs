using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject {
    private const string kStart = "START";
    private const string kEnd = "END";
 
    public struct Response {
        public string displayText;
        public string destinationNode;
        public string messageText;
 
        public Response( string display, string destination, string messageTxt) {
            displayText = display;
            destinationNode = destination;
            messageText = messageTxt;
        }
    }
 
    public class Node {
        public string title;
        public string text;
        public List<string> tags;
        public List<Response> responses;
 
        internal bool IsEndNode() {
            return tags.Contains( kEnd );
        }
 
        // TODO proper override
        public string Print() {
            return "";//string.Format( "Node {  Title: '%s',  Tag: '%s',  Text: '%s'}", title, tag, text );
        }
 
    }
 
    public class Dialogue {
        string title;
        Dictionary<string, Node> nodes;
        string titleOfStartNode;
        
        public Dialogue( TextAsset twineText ) {
            nodes = new Dictionary<string, Node>();
            ParseTwineText( twineText.ToString() );
        }
 
        public Node GetNode( string nodeTitle ) {
            return nodes [ nodeTitle ];
        }
 
        public Node GetStartNode() {
            UnityEngine.Assertions.Assert.IsNotNull( titleOfStartNode );
            return nodes [ titleOfStartNode ];
        }
 
        public void ParseTwineText( string twineText )
        {
            string[] nodeData = twineText.Split(new string[] { "::" }, StringSplitOptions.None);
 
            bool passedHeader = false;
            const int kIndexOfContentStart = 4;
            for ( int i = 0; i < nodeData.Length; i++ )
            {
 
                // The first node comes after the UserStylesheet node
                if ( !passedHeader )
                {
                    if ( nodeData[ i ].StartsWith( " UserStylesheet" ) )
                        passedHeader = true;
 
                    continue;
                }
 
                // Note: tags are optional
                // Normal Format: "NodeTitle [Tags, comma, seperated] \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                // No-Tag Format: "NodeTitle \r\n Message Text \r\n [[Response One]] \r\n [[Response Two]]"
                string currLineText = nodeData[i];
 
                // Remove position data
                int posBegin = currLineText.IndexOf("{\"position");
                if ( posBegin != -1 )
                {
                    int posEnd = currLineText.IndexOf("}", posBegin);
                    currLineText = currLineText.Substring( 0, posBegin ) + currLineText.Substring( posEnd + 1 );
                }

                bool tagsPresent = currLineText.IndexOf( "[" ) < currLineText.IndexOf( "\r\n" );
                int endOfFirstLine = currLineText.IndexOf( "\r\n" );
                
                int startOfResponses = -1;
                int startOfResponseDestinations = currLineText.IndexOf( "[[" );
                bool lastNode = (startOfResponseDestinations == -1);
                if ( lastNode )
                    startOfResponses = currLineText.Length;
                else
                {
                    // Last new line before "[["
                    startOfResponses = currLineText.Substring( 0, startOfResponseDestinations ).LastIndexOf( "\r\n" );
                }
 
                // Extract Title
                int titleStart = 0;
                int titleEnd = tagsPresent
                    ? currLineText.IndexOf( "[" )
                    : endOfFirstLine;
                //UnityEngine.Assertions.Assert.IsTrue( titleEnd > 0, "Maybe you have a node with no responses?" );
                string title = currLineText.Substring(titleStart, titleEnd).Trim();
                
                // Extract Tags (if any)
                string tags = tagsPresent
                    ? currLineText.Substring( titleEnd + 1, (endOfFirstLine - titleEnd)-2)
                    : "";
 
                if ( !string.IsNullOrEmpty( tags ) && tags[ tags.Length - 1 ] == ']' )
                    tags = tags.Substring( 0, tags.Length - 1 );

                Debug.Log("Tags: " + tags);

                // Extract Message Text & Responses
                string messsageText = currLineText.Substring( endOfFirstLine, startOfResponses - endOfFirstLine).Trim();
                string responseText = currLineText.Substring( startOfResponses ).Trim();
 
                Node curNode = new Node();
                curNode.title = title;
                curNode.text = messsageText;
                curNode.tags = new List<string>( tags.Split( new string[] { " " }, StringSplitOptions.None ) );
                //Debug.Log("MessageText: "+messsageText);
                if ( curNode.tags.Contains( kStart ) )
                {
                    UnityEngine.Assertions.Assert.IsTrue( null == titleOfStartNode );
                    titleOfStartNode = curNode.title;
                }
                if ( curNode.tags.Contains( kEnd ) )
                {
                    // Do something
                    
                }
                // Note: response messages are optional (if no message then destination is the message)
                // With Message Format: "\r\n Message[[Response One]]"
                // Message-less Format: "\r\n [[Response One]]"
                curNode.responses = new List<Response>();
                if ( !lastNode )
                {
                    List<string> responseData = new List<string>(responseText.Split( new string [] { "\r\n" }, StringSplitOptions.None ));
                    for ( int k = responseData.Count - 1; k >= 0; k-- )
                    {
                        string curResponseData = responseData[k];
 
                        if ( string.IsNullOrEmpty( curResponseData ) )
                        {
                            responseData.RemoveAt( k );
                            continue;
                        }
 
                        Response curResponse = new Response();
                        //Debug.Log(curResponseData);
                        int destinationStart = curResponseData.IndexOf( "[[" );
                        int destinationEnd = curResponseData.IndexOf( "]]" );
                        UnityEngine.Assertions.Assert.IsFalse( destinationStart == -1, "No destination around in node titled, '" + curNode.title + "'" );
                        UnityEngine.Assertions.Assert.IsFalse( destinationEnd == -1, "No destination around in node titled, '" + curNode.title + "'" );
                        string destination = curResponseData.Substring(destinationStart + 2, (destinationEnd - destinationStart)-2);
                        curResponse.destinationNode = destination;
                        curResponse.messageText = messsageText;
                        if ( destinationStart == -1 )
                        {
                            //Debug.Log("The current response is empty.");
                            curResponse.displayText = ""; // If message-less, then message is an empty string
                        }
                            
                        else
                            curResponse.displayText = curResponseData.Substring( destinationStart +2, destinationEnd-2 );
                        curNode.responses.Add( curResponse );
                    }
                }
 
                nodes[ curNode.title ] = curNode;
            }
        }
    }
}
 