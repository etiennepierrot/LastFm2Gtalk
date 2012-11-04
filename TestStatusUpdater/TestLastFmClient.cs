using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatusUpdater;
using StatusUpdater.LastFM;
using StructureMap;

namespace TestStatusUpdater
{
    [TestClass]
    public class TestLastFmClient
    {
        private LastFmContentParserService _lastFmContentParserService;

        [TestMethod]
        public void ParseResponse()
        {
#region hide
            const string response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lfm status=""ok"">
<recenttracks user=""Etienne_Fab4"" page=""1"" perPage=""10"" totalPages=""6318"" total=""63176"" >
<track nowplaying=""true""> 
                                <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>My Kind of Girl</name>
<streamable>0</streamable>
        <mbid>779f9acb-800e-4ca4-b18a-79af78352d38</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/My+Kind+of+Girl</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
</track>
<track> 
                                    <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>There's Love</name>
<streamable>0</streamable>
        <mbid>acd8f165-3fb5-4fcd-81f6-6b281fc38f21</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/There%27s+Love</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
        <date uts=""1351762665"">1 Nov 2012, 09:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Solitude</name>
<streamable>1</streamable>
        <mbid>07b90c81-c0f6-4482-a6c4-fdaf0ee026f4</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Solitude</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609347"">30 Oct 2012, 15:02</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Got It Bad (And That Ain't Good)</name>
<streamable>1</streamable>
        <mbid>7b2d9169-9c13-4fd6-a0b3-7be47748c22a</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Got+It+Bad+(And+That+Ain%27t+Good)</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609149"">30 Oct 2012, 14:59</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Mood Indigo</name>
<streamable>1</streamable>
        <mbid>012052d6-6a32-467e-b357-9de169faad1d</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Mood+Indigo</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608842"">30 Oct 2012, 14:54</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Just A-Sittin' And A-Rockin'</name>
<streamable>1</streamable>
        <mbid>5ee8a11c-6333-4cec-8f55-9035780565c1</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Just+A-Sittin%27+And+A-Rockin%27</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608631"">30 Oct 2012, 14:50</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Creole Love Call</name>
<streamable>1</streamable>
        <mbid>8dfa3c90-3f2c-4023-ac94-dec950a00708</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Creole+Love+Call</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608363"">30 Oct 2012, 14:46</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>The C Jam Blues</name>
<streamable>1</streamable>
        <mbid></mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/The+C+Jam+Blues</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608203"">30 Oct 2012, 14:43</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Sophisticated Lady</name>
<streamable>1</streamable>
        <mbid>1e0356a9-b7e2-4a22-bed1-8038c381745b</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Sophisticated+Lady</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608032"">30 Oct 2012, 14:40</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Perdido</name>
<streamable>1</streamable>
        <mbid>0985b425-c877-46bc-b3f0-ba9ea543955e</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Perdido</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607842"">30 Oct 2012, 14:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Let a Song Go Out of My Heart</name>
<streamable>1</streamable>
        <mbid>0b2f4ecc-3d59-447a-a597-3b6a4643fbca</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Let+a+Song+Go+Out+of+My+Heart</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607661"">30 Oct 2012, 14:34</date>
</track>
</recenttracks></lfm>
";
#endregion
            string statutResponse = _lastFmContentParserService.ParseStatusResponse(response);
            Assert.AreEqual("ok", statutResponse);

        }

        [TestMethod]
        public void ParseCurrentTrack()
        {
            #region response XML
            const string response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lfm status=""ok"">
<recenttracks user=""Etienne_Fab4"" page=""1"" perPage=""10"" totalPages=""6318"" total=""63176"" >
<track nowplaying=""true""> 
                                <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>My Kind of Girl</name>
<streamable>0</streamable>
        <mbid>779f9acb-800e-4ca4-b18a-79af78352d38</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/My+Kind+of+Girl</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
</track>
<track> 
                                    <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>There's Love</name>
<streamable>0</streamable>
        <mbid>acd8f165-3fb5-4fcd-81f6-6b281fc38f21</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/There%27s+Love</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
        <date uts=""1351762665"">1 Nov 2012, 09:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Solitude</name>
<streamable>1</streamable>
        <mbid>07b90c81-c0f6-4482-a6c4-fdaf0ee026f4</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Solitude</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609347"">30 Oct 2012, 15:02</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Got It Bad (And That Ain't Good)</name>
<streamable>1</streamable>
        <mbid>7b2d9169-9c13-4fd6-a0b3-7be47748c22a</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Got+It+Bad+(And+That+Ain%27t+Good)</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609149"">30 Oct 2012, 14:59</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Mood Indigo</name>
<streamable>1</streamable>
        <mbid>012052d6-6a32-467e-b357-9de169faad1d</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Mood+Indigo</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608842"">30 Oct 2012, 14:54</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Just A-Sittin' And A-Rockin'</name>
<streamable>1</streamable>
        <mbid>5ee8a11c-6333-4cec-8f55-9035780565c1</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Just+A-Sittin%27+And+A-Rockin%27</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608631"">30 Oct 2012, 14:50</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Creole Love Call</name>
<streamable>1</streamable>
        <mbid>8dfa3c90-3f2c-4023-ac94-dec950a00708</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Creole+Love+Call</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608363"">30 Oct 2012, 14:46</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>The C Jam Blues</name>
<streamable>1</streamable>
        <mbid></mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/The+C+Jam+Blues</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608203"">30 Oct 2012, 14:43</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Sophisticated Lady</name>
<streamable>1</streamable>
        <mbid>1e0356a9-b7e2-4a22-bed1-8038c381745b</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Sophisticated+Lady</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608032"">30 Oct 2012, 14:40</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Perdido</name>
<streamable>1</streamable>
        <mbid>0985b425-c877-46bc-b3f0-ba9ea543955e</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Perdido</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607842"">30 Oct 2012, 14:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Let a Song Go Out of My Heart</name>
<streamable>1</streamable>
        <mbid>0b2f4ecc-3d59-447a-a597-3b6a4643fbca</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Let+a+Song+Go+Out+of+My+Heart</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607661"">30 Oct 2012, 14:34</date>
</track>
</recenttracks></lfm>
";
            #endregion

            var track = _lastFmContentParserService.ParseCurrentTrack(response);
            Assert.AreEqual("My Kind of Girl", track.Song);
        }

        [TestMethod]
        public void ParseCurrentTrack2()
        {
            #region response XML
            string response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lfm status=""ok"">
<recenttracks user=""Etienne_Fab4"" page=""1"" perPage=""10"" totalPages=""6318"" total=""63176"" >
<track nowplaying=""true""> 
                                <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>L-O-V-E</name>
<streamable>0</streamable>
        <mbid>779f9acb-800e-4ca4-b18a-79af78352d38</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/My+Kind+of+Girl</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
</track>
<track> 
                                    <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>There's Love</name>
<streamable>0</streamable>
        <mbid>acd8f165-3fb5-4fcd-81f6-6b281fc38f21</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/There%27s+Love</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
        <date uts=""1351762665"">1 Nov 2012, 09:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Solitude</name>
<streamable>1</streamable>
        <mbid>07b90c81-c0f6-4482-a6c4-fdaf0ee026f4</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Solitude</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609347"">30 Oct 2012, 15:02</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Got It Bad (And That Ain't Good)</name>
<streamable>1</streamable>
        <mbid>7b2d9169-9c13-4fd6-a0b3-7be47748c22a</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Got+It+Bad+(And+That+Ain%27t+Good)</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609149"">30 Oct 2012, 14:59</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Mood Indigo</name>
<streamable>1</streamable>
        <mbid>012052d6-6a32-467e-b357-9de169faad1d</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Mood+Indigo</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608842"">30 Oct 2012, 14:54</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Just A-Sittin' And A-Rockin'</name>
<streamable>1</streamable>
        <mbid>5ee8a11c-6333-4cec-8f55-9035780565c1</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Just+A-Sittin%27+And+A-Rockin%27</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608631"">30 Oct 2012, 14:50</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Creole Love Call</name>
<streamable>1</streamable>
        <mbid>8dfa3c90-3f2c-4023-ac94-dec950a00708</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Creole+Love+Call</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608363"">30 Oct 2012, 14:46</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>The C Jam Blues</name>
<streamable>1</streamable>
        <mbid></mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/The+C+Jam+Blues</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608203"">30 Oct 2012, 14:43</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Sophisticated Lady</name>
<streamable>1</streamable>
        <mbid>1e0356a9-b7e2-4a22-bed1-8038c381745b</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Sophisticated+Lady</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608032"">30 Oct 2012, 14:40</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Perdido</name>
<streamable>1</streamable>
        <mbid>0985b425-c877-46bc-b3f0-ba9ea543955e</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Perdido</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607842"">30 Oct 2012, 14:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Let a Song Go Out of My Heart</name>
<streamable>1</streamable>
        <mbid>0b2f4ecc-3d59-447a-a597-3b6a4643fbca</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Let+a+Song+Go+Out+of+My+Heart</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607661"">30 Oct 2012, 14:34</date>
</track>
</recenttracks></lfm>
";
            #endregion

            var track = _lastFmContentParserService.ParseCurrentTrack(response);
            Assert.AreEqual("L-O-V-E", track.Song);
        }

        [TestMethod]
        public void ParseNodeCurrentTrack()
        {
            #region response XML
            string response = @"<?xml version=""1.0"" encoding=""utf-8""?>
<lfm status=""ok"">
<recenttracks user=""Etienne_Fab4"" page=""1"" perPage=""10"" totalPages=""6318"" total=""63176"" >
<track nowplaying=""true""> 
                                <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>L-O-V-E</name>
<streamable>0</streamable>
        <mbid>779f9acb-800e-4ca4-b18a-79af78352d38</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/My+Kind+of+Girl</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
</track>
<track> 
                                    <artist mbid=""80c7de6a-0415-410f-ac8d-4b6406af87a6"">Nat King Cole</artist>
            <name>There's Love</name>
<streamable>0</streamable>
        <mbid>acd8f165-3fb5-4fcd-81f6-6b281fc38f21</mbid>
            <album mbid=""935b657b-3a3b-4744-8c82-656dd6563b7a"">L-O-V-E</album>
    <url>http://www.last.fm/music/Nat+King+Cole/_/There%27s+Love</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/45739683.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/45739683.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/45739683.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/45739683.jpg</image>
        <date uts=""1351762665"">1 Nov 2012, 09:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Solitude</name>
<streamable>1</streamable>
        <mbid>07b90c81-c0f6-4482-a6c4-fdaf0ee026f4</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Solitude</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609347"">30 Oct 2012, 15:02</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Got It Bad (And That Ain't Good)</name>
<streamable>1</streamable>
        <mbid>7b2d9169-9c13-4fd6-a0b3-7be47748c22a</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Got+It+Bad+(And+That+Ain%27t+Good)</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351609149"">30 Oct 2012, 14:59</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Mood Indigo</name>
<streamable>1</streamable>
        <mbid>012052d6-6a32-467e-b357-9de169faad1d</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Mood+Indigo</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608842"">30 Oct 2012, 14:54</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Just A-Sittin' And A-Rockin'</name>
<streamable>1</streamable>
        <mbid>5ee8a11c-6333-4cec-8f55-9035780565c1</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Just+A-Sittin%27+And+A-Rockin%27</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608631"">30 Oct 2012, 14:50</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Creole Love Call</name>
<streamable>1</streamable>
        <mbid>8dfa3c90-3f2c-4023-ac94-dec950a00708</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Creole+Love+Call</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608363"">30 Oct 2012, 14:46</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>The C Jam Blues</name>
<streamable>1</streamable>
        <mbid></mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/The+C+Jam+Blues</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608203"">30 Oct 2012, 14:43</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Sophisticated Lady</name>
<streamable>1</streamable>
        <mbid>1e0356a9-b7e2-4a22-bed1-8038c381745b</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Sophisticated+Lady</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351608032"">30 Oct 2012, 14:40</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>Perdido</name>
<streamable>1</streamable>
        <mbid>0985b425-c877-46bc-b3f0-ba9ea543955e</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/Perdido</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607842"">30 Oct 2012, 14:37</date>
</track>
<track> 
                                    <artist mbid=""3af06bc4-68ad-4cae-bb7a-7eeeb45e411f"">Duke Ellington</artist>
            <name>I Let a Song Go Out of My Heart</name>
<streamable>1</streamable>
        <mbid>0b2f4ecc-3d59-447a-a597-3b6a4643fbca</mbid>
            <album mbid="""">Take the A Train</album>
    <url>http://www.last.fm/music/Duke+Ellington/_/I+Let+a+Song+Go+Out+of+My+Heart</url>
    <image size=""small"">http://userserve-ak.last.fm/serve/34s/80142275.jpg</image>
    <image size=""medium"">http://userserve-ak.last.fm/serve/64s/80142275.jpg</image>
    <image size=""large"">http://userserve-ak.last.fm/serve/126/80142275.jpg</image>
    <image size=""extralarge"">http://userserve-ak.last.fm/serve/300x300/80142275.jpg</image>
        <date uts=""1351607661"">30 Oct 2012, 14:34</date>
</track>
</recenttracks></lfm>
";
            #endregion

            var node = _lastFmContentParserService.GetNodeCurrentTrack(response);
            Assert.IsNotNull(node);
        }



        [TestInitialize]
        public void Initialize()
        {
            ObjectFactory.Configure(init => init.AddRegistry(new StructureMapConfigurationRegistry()));
            _lastFmContentParserService = ObjectFactory.GetInstance<LastFmContentParserService>();
        }
    }
}
