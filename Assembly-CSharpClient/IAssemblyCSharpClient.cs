﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Assembly-CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Microsoft.Rest;

    /// <summary>
    /// The Computer Vision API provides state-of-the-art algorithms to
    /// process images and return information.  For example, it can be used
    /// to determine if an image contains mature content, or it can be used
    /// to find all the faces in an image.  It also has other features like
    /// estimating dominant and accent colors, categorizing the content of
    /// images, and describing an image with complete English sentences.
    /// Additionally, it can also intelligently generate images thumbnails
    /// for displaying large images effectively.
    /// 
    /// This API is currently available in:
    /// 
    /// * Australia East - australiaeast.api.cognitive.microsoft.com
    /// * Brazil South - brazilsouth.api.cognitive.microsoft.com
    /// * Canada Central - canadacentral.api.cognitive.microsoft.com
    /// * Central India - centralindia.api.cognitive.microsoft.com
    /// * Central US - centralus.api.cognitive.microsoft.com
    /// * East Asia - eastasia.api.cognitive.microsoft.com
    /// * East US - eastus.api.cognitive.microsoft.com
    /// * East US 2 - eastus2.api.cognitive.microsoft.com
    /// * France Central - francecentral.api.cognitive.microsoft.com
    /// * Japan East - japaneast.api.cognitive.microsoft.com
    /// * Japan West - japanwest.api.cognitive.microsoft.com
    /// * Korea Central - koreacentral.api.cognitive.microsoft.com
    /// * North Central US - northcentralus.api.cognitive.microsoft.com
    /// * North Europe - northeurope.api.cognitive.microsoft.com
    /// * South Africa North - southafricanorth.api.cognitive.microsoft.com
    /// * South Central US - southcentralus.api.cognitive.microsoft.com
    /// * Southeast Asia - southeastasia.api.cognitive.microsoft.com
    /// * UK South - uksouth.api.cognitive.microsoft.com
    /// * West Central US - westcentralus.api.cognitive.microsoft.com
    /// * West Europe - westeurope.api.cognitive.microsoft.com
    /// * West US - westus.api.cognitive.microsoft.com
    /// * West US 2 - westus2.api.cognitive.microsoft.com
    /// </summary>
    public partial interface IAssemblyCSharpClient : IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Subscription credentials which uniquely identify client
        /// subscription.
        /// </summary>
        ServiceClientCredentials Credentials { get; }


            /// <summary>
        /// Analyze Image
        /// </summary>
        /// This operation extracts a rich set of visual features based on the
        /// image content.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// Two input methods are supported -- (1) Uploading an image
        /// or (2) specifying an image URL.  Within your request, there is an
        /// optional parameter to allow you to choose which features to
        /// return.  By default, image categories are returned in the
        /// response.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON.  If the
        /// request failed, the response will contain an error code and a
        /// message to help understand what went wrong.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='visualFeatures'>
        /// A string indicating what visual feature types to return. Multiple
        /// values should be comma-separated.
        /// &lt;br/&gt;Valid visual feature types include:
        /// &lt;br/&gt;
        /// &lt;ul&gt;
        /// &lt;li&gt;&lt;b&gt;Adult&lt;/b&gt; - detects if the image is
        /// pornographic in nature (depicts nudity or a sex act).  Sexually
        /// suggestive content is also detected.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Brands&lt;/b&gt; - detects various brands
        /// within an image, including the approximate location. The Brands
        /// argument is only available in English.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Categories&lt;/b&gt; - categorizes image
        /// content according to a taxonomy defined in documentation.
        /// &lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Color&lt;/b&gt; - determines the accent color,
        /// dominant color, and whether an image is
        /// black&amp;white.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Description&lt;/b&gt; - describes the image
        /// content with a complete sentence in supported languages.
        /// &lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Faces&lt;/b&gt; - detects if faces are present.
        /// If present, generate coordinates, gender and age.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;ImageType&lt;/b&gt; - detects if image is
        /// clipart or a line drawing.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Objects&lt;/b&gt; - detects various objects
        /// within an image, including the approximate location. The Objects
        /// argument is only available in English.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;Tags&lt;/b&gt; - tags the image with a detailed
        /// list of words related to the image content. &lt;/li&gt;
        /// &lt;/ul&gt;. Possible values include: 'Adult', 'Brands',
        /// 'Categories', 'Color', 'Description', 'Faces', 'ImageType',
        /// 'Objects', 'Tags'
        /// </param>
        /// <param name='details'>
        /// A string indicating which domain-specific details to return.
        /// Multiple values should be comma-separated.
        /// &lt;br/&gt;Valid visual feature types include:
        /// &lt;br/&gt;
        /// &lt;ul&gt;
        /// &lt;li&gt;&lt;b &gt;Celebrities&lt;/b&gt; - identifies celebrities
        /// if detected in the image.&lt;/li&gt;
        /// &lt;li&gt;&lt;b &gt;Landmarks&lt;/b&gt; - identifies landmarks if
        /// detected in the image.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// . Possible values include: 'Celebrities', 'Landmarks'
        /// </param>
        /// <param name='language'>
        /// A string indicating which language to return. The service will
        /// return recognition results in specified language. If this
        /// parameter is not specified, the default value is
        /// &amp;quot;en&amp;quot;.&lt;br /&gt;
        /// Supported languages:
        /// &lt;ul&gt;
        /// &lt;li&gt;&lt;b&gt;en&lt;/b&gt; - English, Default.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;es&lt;/b&gt; - Spanish.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;ja&lt;/b&gt; - Japanese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;pt&lt;/b&gt; - Portuguese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;zh&lt;/b&gt; - Simplified Chinese.&lt;/li&gt;
        /// &lt;/ul&gt;. Possible values include: 'en', 'es', 'ja', 'pt', 'zh'
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions must be at least 50 x 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeOnefaWithHttpMessagesAsync(string visualFeatures = "Categories", string details = default(string), string language = "en", object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Describe Image
        /// </summary>
        /// This operation generates a description of an image in human
        /// readable language with complete sentences.  The description is
        /// based on a collection of content tags, which are also returned by
        /// the operation. More than one description can be generated for
        /// each image.  Descriptions are ordered by their confidence score.
        /// All descriptions are in English.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// Two input methods are supported -- (1) Uploading an image
        /// or (2) specifying an image URL.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON.  If the
        /// request failed, the response will contain an error code and a
        /// message to help understand what went wrong.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='maxCandidates'>
        /// Maximum number of candidate descriptions to be returned.  The
        /// default is 1. Possible values include: '1'
        /// </param>
        /// <param name='language'>
        /// A string indicating the language in which the service will return
        /// a description of the image. If this parameter is not specified,
        /// the default value is &amp;quot;en&amp;quot;.&lt;br /&gt;
        /// Supported languages:
        /// &lt;ul&gt;
        /// &lt;li&gt;&lt;b&gt;en&lt;/b&gt; - English, Default.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;es&lt;/b&gt; - Spanish.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;ja&lt;/b&gt; - Japanese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;pt&lt;/b&gt; - Portuguese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;zh&lt;/b&gt; - Simplified Chinese.&lt;/li&gt;
        /// &lt;/ul&gt;. Possible values include: 'en', 'es', 'ja', 'pt', 'zh'
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions should be greater than 50 x
        /// 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeOnefeWithHttpMessagesAsync(string maxCandidates = "1", string language = "en", object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Recognize Text Operation Result
        /// </summary>
        /// This interface is used for getting recognize text operation
        /// result. The URL to this interface should be retrieved from
        /// &lt;b&gt;"Operation-Location"&lt;/b&gt; field returned from
        /// Recognize Text interface.
        /// <param name='operationId'>
        /// Id of the text operation returned in the response of the &lt;a
        /// href="/docs/services/56f91f2d778daf23d8ec6739/operations/587f2c6a154055056008f200"&gt;Recognize
        /// Text&lt;/a&gt; interface.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveEightSevenfTwocfOneOneFiveFourZeroFiveFiveZeroFiveSixZeroZeroEightfTwoZeroOneWithHttpMessagesAsync(string operationId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Thumbnail
        /// </summary>
        /// This operation generates a thumbnail image with the user-specified
        /// width and height.  By default, the service analyzes the image,
        /// identifies the region of interest (ROI), and generates smart
        /// cropping coordinates based on the ROI.  Smart cropping helps when
        /// you specify an aspect ratio that differs from that of the input
        /// image
        /// &lt;p/&gt;
        /// A successful response contains the thumbnail image binary.  If the
        /// request failed, the response contains an error code and a message
        /// to help determine what went wrong.
        /// 
        /// &lt;p/&gt;
        /// Upon failure, the error code and an error message are returned.
        /// The error code could be one of InvalidImageUrl,
        /// InvalidImageFormat, InvalidImageSize, InvalidThumbnailSize,
        /// NotSupportedImage, FailedToProcess, Timeout, or
        /// InternalServerError.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='width'>
        /// Width of the thumbnail.  It must be between 1 and 1024.
        /// Recommended minimum of 50.
        /// </param>
        /// <param name='height'>
        /// Height of the thumbnail. It must be between 1 and 1024.
        /// Recommended minimum of 50.
        /// </param>
        /// <param name='smartCropping'>
        /// Boolean flag for enabling smart cropping.
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions should be greater than 50 x
        /// 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeOnefbWithHttpMessagesAsync(double width, double height, bool? smartCropping = true, object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List Domain Specific Models
        /// </summary>
        /// This operation returns the list of domain-specific models that are
        /// supported by the Computer Vision API.  Currently, the API
        /// supports following domain-specific models: celebrity recognizer,
        /// landmark recognizer.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON.  If the
        /// request failed, the response will contain an error code and a
        /// message to help understand what went wrong.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// GET
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeOnefdWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// OCR
        /// </summary>
        /// Optical Character Recognition (OCR) detects text in an image and
        /// extracts the recognized characters into a machine-usable
        /// character stream.
        /// 
        /// &lt;p/&gt;
        /// Upon success, the OCR results will be returned.
        /// &lt;p/&gt;
        /// Upon failure, the error code together with an error message will
        /// be returned. The error code can be one of InvalidImageUrl,
        /// InvalidImageFormat, InvalidImageSize, NotSupportedImage,
        /// NotSupportedLanguage, or InternalServerError.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='language'>
        /// The BCP-47 language code of the text to be detected in the
        /// image.The default value is &amp;quot;unk&amp;quot;, then the
        /// service will auto detect the language of the text in the
        /// image.&lt;br /&gt;
        /// &lt;br /&gt;
        /// Supported languages:
        /// &lt;ul
        /// style="margin-left:.375in;direction:ltr;unicode-bidi:embed;
        /// margin-top:0in;margin-bottom:0in" type="disc"&gt;
        /// &lt;li&gt;unk (AutoDetect)&lt;/li&gt;
        /// &lt;li&gt;zh-Hans (ChineseSimplified)&lt;/li&gt;
        /// &lt;li&gt;zh-Hant (ChineseTraditional)&lt;/li&gt;
        /// &lt;li&gt;cs (Czech)&lt;/li&gt;
        /// &lt;li&gt;da (Danish)&lt;/li&gt;
        /// &lt;li&gt;nl (Dutch)&lt;/li&gt;
        /// &lt;li&gt;en (English)&lt;/li&gt;
        /// &lt;li&gt;fi (Finnish)&lt;/li&gt;
        /// &lt;li&gt;fr (French)&lt;/li&gt;
        /// &lt;li&gt;de (German)&lt;/li&gt;
        /// &lt;li&gt;el (Greek)&lt;/li&gt;
        /// &lt;li&gt;hu (Hungarian)&lt;/li&gt;
        /// &lt;li&gt;it (Italian)&lt;/li&gt;
        /// &lt;li&gt;ja (Japanese)&lt;/li&gt;
        /// &lt;li&gt;ko (Korean)&lt;/li&gt;
        /// &lt;li&gt;nb (Norwegian)&lt;/li&gt;
        /// &lt;li&gt;pl (Polish)&lt;/li&gt;
        /// &lt;li&gt;pt (Portuguese,&lt;/li&gt;
        /// &lt;li&gt;ru (Russian)&lt;/li&gt;
        /// &lt;li&gt;es (Spanish)&lt;/li&gt;
        /// &lt;li&gt;sv (Swedish)&lt;/li&gt;
        /// &lt;li&gt;tr (Turkish)&lt;/li&gt;
        /// &lt;li&gt;ar (Arabic)&lt;/li&gt;
        /// &lt;li&gt;ro (Romanian)&lt;/li&gt;
        /// &lt;li&gt;sr-Cyrl (SerbianCyrillic)&lt;/li&gt;
        /// &lt;li&gt;sr-Latn (SerbianLatin)&lt;/li&gt;
        /// &lt;li&gt;sk (Slovak)&lt;/li&gt;
        /// &lt;/ul&gt;. Possible values include: 'unk', 'zh-Hans', 'zh-Hant',
        /// 'cs', 'da', 'nl', 'en', 'fi', 'fr', 'de', 'el', 'hu', 'it', 'ja',
        /// 'ko', 'nb', 'pl', 'pt', 'ru', 'es', 'sv', 'tr', 'ar', 'ro',
        /// 'sr-Cyrl', 'sr-Latn', 'sk'
        /// </param>
        /// <param name='detectOrientation'>
        /// Whether detect the text orientation in the image. With
        /// detectOrientation=true the OCR service tries to detect the image
        /// orientation and correct it before further processing (e.g. if
        /// it's upside-down).
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP.
        /// &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions must be between 50 x 50 and 4200 x
        /// 4200 pixels, and the image cannot be larger than 10
        /// megapixels.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeOnefcWithHttpMessagesAsync(string language = "unk", bool? detectOrientation = true, object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Recognize Domain Specific Content
        /// </summary>
        /// This operation recognizes content within an image by applying a
        /// domain-specific model.  The list of domain-specific models that
        /// are supported by the Computer Vision API can be retrieved using
        /// the /models GET request.  Currently, the API provides following
        /// domain-specific models: celebrities, landmarks.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// Two input methods are supported -- (1) Uploading an image
        /// or (2) specifying an image URL.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON.  If the
        /// request failed, the response will contain an error code and a
        /// message to help understand what went wrong.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='model'>
        /// The domain-specific content to recognize.
        /// </param>
        /// <param name='language'>
        /// A string indicating the language in which to return analysis
        /// results, if supported. If this parameter is not specified, the
        /// default value is &amp;quot;en&amp;quot;.&lt;br /&gt;
        /// Possible language values:
        /// &lt;ul&gt;
        /// &lt;li&gt;&lt;b&gt;en&lt;/b&gt; - English, Default.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;es&lt;/b&gt; - Spanish.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;ja&lt;/b&gt; - Japanese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;pt&lt;/b&gt; - Portuguese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;zh&lt;/b&gt; - Simplified Chinese.&lt;/li&gt;
        /// &lt;/ul&gt;. Possible values include: 'en', 'es', 'ja', 'pt', 'zh'
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions should be greater than 50 x
        /// 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeTwoZeroZeroWithHttpMessagesAsync(string model, string language = "en", object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Recognize Text
        /// </summary>
        /// Use this interface to get the result of a Recognize Text
        /// operation. When you use the Recognize Text interface, the
        /// response contains a field called "Operation-Location". The
        /// "Operation-Location" field contains the URL that you must use for
        /// your Get Recognize Text Operation Result operation.
        /// &lt;br/&gt;
        /// &lt;br/&gt;
        /// For the result of a Recognize Text operation to be available, it
        /// requires an amount of time that depends on the length of the
        /// text. So, you may need to wait before using this Get Recognize
        /// Text Operation Result interface. The time you need to wait may be
        /// up to a number of seconds.
        /// &lt;br/&gt;
        /// &lt;br/&gt;
        /// Note: this technology is currently in preview and is only
        /// available for English text.
        /// <param name='mode'>
        /// If this parameter is set to "Printed", printed text recognition is
        /// performed. If "Handwritten" is specified, handwriting recognition
        /// is performed. (Note: This parameter is case sensitive.) This is a
        /// required parameter and cannot be empty.​. Possible values
        /// include: 'Handwritten', 'Printed'
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG and BMP.
        /// &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions must be at least 50 x 50, at most
        /// 4200 x 4200.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveEightSevenfTwocSixaOneFiveFourZeroFiveFiveZeroFiveSixZeroZeroEightfTwoZeroZeroWithHttpMessagesAsync(string mode, object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Tag Image
        /// </summary>
        /// This operation generates a list of words, or tags, that are
        /// relevant to the content of the supplied image. The Computer
        /// Vision API can return tags based on objects, living beings,
        /// scenery or actions found in images. Unlike categories, tags are
        /// not organized according to a hierarchical classification system,
        /// but correspond to image content. Tags may contain hints to avoid
        /// ambiguity or provide context, for example the tag "ascomycete"
        /// may be accompanied by the hint "fungus".
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// Two input methods are supported -- (1) Uploading an image
        /// or (2) specifying an image URL.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON.  If the
        /// request failed, the response will contain an error code and a
        /// message to help understand what went wrong.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='language'>
        /// A string indicating the language in which to return tags. If this
        /// parameter is not specified, the default value is
        /// &amp;quot;en&amp;quot;.&lt;br /&gt;
        /// Supported languages:
        /// &lt;ul&gt;
        /// &lt;li&gt;&lt;b&gt;en&lt;/b&gt; - English, Default.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;es&lt;/b&gt; - Spanish.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;ja&lt;/b&gt; - Japanese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;pt&lt;/b&gt; - Portuguese.&lt;/li&gt;
        /// &lt;li&gt;&lt;b&gt;zh&lt;/b&gt; - Simplified Chinese.&lt;/li&gt;
        /// &lt;/ul&gt;. Possible values include: 'en', 'es', 'ja', 'pt', 'zh'
        /// </param>
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions should be greater than 50 x
        /// 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveSixfNineOnefTwoeSevenSevenEightdafOneFouraFourNineNineeOneffWithHttpMessagesAsync(string language = "en", object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Area of Interest
        /// </summary>
        /// This operation returns a bounding box around the most important
        /// area of the image.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON.  Upon
        /// failure, the error code and an error message are returned. The
        /// error code could be one of InvalidImageUrl, InvalidImageFormat,
        /// InvalidImageSize, InvalidThumbnailSize, NotSupportedImage,
        /// FailedToProcess, Timeout, or InternalServerError.
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions must be at least 50 x 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> B156d0f5e11e492d9f64307cWithHttpMessagesAsync(object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Detect Objects
        /// </summary>
        /// This operation Performs object detection on the specified image.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// Two input methods are supported -- (1) Uploading an image
        /// or (2) specifying an image URL.
        /// &lt;br&gt;
        /// &lt;br&gt;
        /// A successful response will be returned in JSON. If the
        /// request failed, the response will contain an error code and a
        /// message to help understand what went wrong.
        /// 
        /// &lt;h4&gt;Http Method&lt;/h4&gt;
        /// POST
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, GIF, BMP. &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 4MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions must be at least 50 x 50.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FiveeZerocdedaSevenSevenaEightFourfcdNineaSixdThreedZeroaWithHttpMessagesAsync(object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Batch Read File
        /// </summary>
        /// Use this interface to get the result of a Batch Read File
        /// operation, employing the state-of-the-art Optical Character
        /// Recognition (OCR) algorithms optimized for text-heavy documents.
        /// It can handle hand-written, printed or mixed documents.
        /// When you use the Batch Read File interface, the response contains
        /// a field called "Operation-Location". The "Operation-Location"
        /// field contains the URL that you must use
        /// for your &lt;a
        /// href="/docs/services/5adf991815e1060e6355ad44/operations/5be108e7498a4f9ed20bf96d"&gt;Get
        /// Read Operation
        /// Result&lt;/a&gt; operation to access OCR results.​
        /// &lt;br/&gt;
        /// &lt;br/&gt;
        /// For the result of a Batch Read File operation to be available, it
        /// requires an amount of time that depends on the length
        /// of the text and the page count. So, you may need to wait before
        /// using the &lt;a
        /// href="/docs/services/5adf991815e1060e6355ad44/operations/5be108e7498a4f9ed20bf96d"&gt;Get
        /// Read Operation Result&lt;/a&gt;
        /// operation. The time you need to wait may be up to a few minutes
        /// for text-heavy, multi-page images. ​
        /// &lt;br/&gt;
        /// &lt;br/&gt;
        /// Note: this technology is only available for English text.
        /// <param name='body'>
        /// Input passed within the POST body. Supported input methods: raw
        /// image binary or image URL.
        /// &lt;br/&gt;
        /// &lt;br/&gt;Input requirements:
        /// &lt;ul&gt;
        /// &lt;li&gt;Supported image formats: JPEG, PNG, BMP, PDF and
        /// TIFF. &lt;/li&gt;
        /// &lt;li&gt;
        /// For PDF and TIFF, up to 200 pages are processed.
        /// &lt;ul&gt;
        /// &lt;li&gt;For free tier subscribers, only the first 2
        /// pages are processed.&lt;/li&gt;
        /// &lt;/ul&gt;
        /// &lt;/li&gt;
        /// &lt;li&gt;Image file size must be less than 20 MB.&lt;/li&gt;
        /// &lt;li&gt;Image dimensions must be at least 50 x 50 pixels and
        /// at most 10000 x 10000 pixels. PDF dimensions must be at most 17 x
        /// 17 inches, corresponding to Legal or A3 paper sizes and
        /// smaller&lt;/li&gt;
        /// &lt;/ul&gt;
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> TwoafbFourNineEightZeroEightNinefSevenFourZeroEightZerodSevenefEightFiveebWithHttpMessagesAsync(object body = default(object), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Read Operation Result
        /// </summary>
        /// This interface is used for getting OCR results of Read operation.
        /// The URL to this interface should be retrieved from
        /// &lt;b&gt;"Operation-Location"&lt;/b&gt; field returned from &lt;a
        /// href="/docs/services/5adf991815e1060e6355ad44/operations/2afb498089f74080d7ef85eb"&gt;Batch
        /// Read File&lt;/a&gt; interface.
        /// 
        /// <param name='operationId'>
        /// Id of read operation returned in the response of the &lt;a
        /// href="/docs/services/5adf991815e1060e6355ad44/operations/2afb498089f74080d7ef85eb"&gt;Batch
        /// Read File&lt;/a&gt; interface.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> FivebeOneZeroEighteSevenFourNineEightaFourfNineedTwoZerobfNineSixdWithHttpMessagesAsync(string operationId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
