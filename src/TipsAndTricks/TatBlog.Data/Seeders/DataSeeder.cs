using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BlogDbContext _dbContext;

        public DataSeeder(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Posts.Any())
            {
                return;
            }

            var authors = AddAuthors();
            var categories = AddCategories();
            var tags = AddTags();
            var posts = AddPosts(authors, categories, tags);

            //var posts = AddPosts(_dbContext.Authors.ToList(), _dbContext.Categories.ToList(), _dbContext.Tags.ToList());
        }

        private IList<Post> AddPosts(IList<Author> authors, IList<Category> categories, IList<Tag> tags)
        {
            var posts = new List<Post>()
            {
                new()
                {
                    Title = "ASP.NET Core Diagnostic Scenarios",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "6 Productivity Shortcuts on Windows 10 & 11",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2022, 9, 25, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 14,
                    Author = authors[1],
                    Category = categories[3],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                },
                new()
                {
                    Title = "Azure Virtual Machines vs App Services",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2022, 7, 19, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 6,
                    Author = authors[0],
                    Category = categories[3],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "Array of object JSON deserialization",
                    ShortDescription = "David and friends has a great repository filled",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository filled",
                    UrlSlug = "aspnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2022, 11, 5, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 19,
                    Author = authors[0],
                    Category = categories[5],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                },
                new()
                {
                    Title = "HoneCtrl: Công cụ tối ưu Windows cực kỳ hiệu quả",
                    ShortDescription = "HoneCtrl",
                    Description = "Công cụ tối ưu Windows",
                    Meta = "",
                    UrlSlug = "honectrl-cong-cu-toi-uu-windows-cuc-ky-hieu-qua",
                    Published = true,
                    PostedDate = new DateTime(2023, 12, 11, 9, 8, 7),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title = "Cách sử dụng Bot Rose Telegram",
                    ShortDescription = "Sử dụng Bot Rose Telegram",
                    Description = "Cách sử dụng Bot Rose Telegram",
                    Meta = "",
                    UrlSlug = "cach-su-dung-bot-rose-telegram",
                    Published = true,
                    PostedDate = new DateTime(2022, 6, 5, 4, 3, 2),
                    ModifiedDate = null,
                    ViewCount = 11,
                    Author = authors[3],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[5]
                    }
                },
                new()
                {
                    Title = "Hướng dẫn nhận 10TB lưu trữ trên Linkbox.to",
                    ShortDescription = "10TB lưu trữ",
                    Description = "Hướng dẫn nhận 10TB lưu trữ trên Linkbox.to",
                    Meta = "",
                    UrlSlug = "huong-dan-nhan-10tb-luu-tru-tren-linkboxto",
                    Published = true,
                    PostedDate = new DateTime(2022, 1, 12, 11, 10, 9),
                    ModifiedDate = null,
                    ViewCount = 12,
                    Author = authors[6],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[8]
                    }
                },
                new()
                {
                    Title = "Cách kiểm tra ổ cứng SSD/HDD khi mới mua và đang sử dụng",
                    ShortDescription = "Kiểm tra ổ cứng SSD/HDD",
                    Description = "Cách kiểm tra ổ cứng SSD/HDD khi mới mua và đang sử dụng",
                    Meta = "",
                    UrlSlug = "cach-kiem-tra-o-cung-ssdhdd-khi-moi-mua-va-dang-su-dung",
                    Published = true,
                    PostedDate = new DateTime(2022, 8, 7, 6, 5, 4),
                    ModifiedDate = null,
                    ViewCount = 13,
                    Author = authors[9],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[11]
                    }
                },
                new()
                {
                    Title = "Cách đặt ảnh động làm hình nền máy tính, Laptop",
                    ShortDescription = "Cài đặt hình nền",
                    Description = "Cách đặt ảnh động làm hình nền máy tính, Laptop",
                    Meta = "",
                    UrlSlug = "cach-dat-anh-dong-lam-hinh-nen-may-tinh-laptop",
                    Published = true,
                    PostedDate = new DateTime(2022, 3, 2, 1, 12, 11),
                    ModifiedDate = null,
                    ViewCount = 14,
                    Author = authors[11],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[9]
                    }
                },
                new()
                {
                    Title = "Tạo hình ảnh độc đáo bằng Bing Image Creator của Microsoft",
                    ShortDescription = "Tạo hình nền",
                    Description = "Tạo hình ảnh độc đáo bằng Bing Image Creator của Microsoft",
                    Meta = "",
                    UrlSlug = "tao-hinh-anh-doc-dao-bang-bing-image-creator-cua-microsoft",
                    Published = true,
                    PostedDate = new DateTime(2022, 10, 9, 8, 7, 6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[8],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[6]
                    }
                },
                new()
                {
                    Title = "Ngắm nhìn nhà bạn từ trên cao với Google Earth",
                    ShortDescription = "Google Earth",
                    Description = "Ngắm nhìn nhà bạn từ trên cao với Google Earth",
                    Meta = "",
                    UrlSlug = "ngam-nhin-nha-ban-tu-tren-cao-voi-google-earth",
                    Published = true,
                    PostedDate = new DateTime(2022, 5, 4, 3, 2, 1),
                    ModifiedDate = null,
                    ViewCount = 16,
                    Author = authors[5],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[3]
                    }
                },
                new()
                {
                    Title = "Chuyển dữ liệu từ điện thoại đến máy tính đơn giản với Nearby Share",
                    ShortDescription = "Chuyển dữ liệu",
                    Description = "Chuyển dữ liệu từ điện thoại đến máy tính đơn giản với Nearby Share",
                    Meta = "",
                    UrlSlug = "chuyen-du-lieu-tu-dien-thoai-den-may-tinh-don-gian-voi-nearby-share",
                    Published = true,
                    PostedDate = new DateTime(2022, 12, 11, 10, 9, 8),
                    ModifiedDate = null,
                    ViewCount = 17,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "Hướng dẫn tự tạo Bot ChatGPT trên Messenger",
                    ShortDescription = "Bot ChatGPT",
                    Description = "Hướng dẫn tự tạo Bot ChatGPT trên Messenger",
                    Meta = "",
                    UrlSlug = "huong-dan-tu-tao-bot-chatgpt-tren-messenger",
                    Published = true,
                    PostedDate = new DateTime(2022, 7, 6, 5, 4, 3),
                    ModifiedDate = null,
                    ViewCount = 18,
                    Author = authors[0],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title = "GooGPT: Trang web kết hợp giữa Google và ChatGPT",
                    ShortDescription = "GooGPT",
                    Description = "GooGPT: Trang web kết hợp giữa Google và ChatGPT",
                    Meta = "",
                    UrlSlug = "googpt-trang-web-ket-hop-giua-google-va-chatgpt",
                    Published = true,
                    PostedDate = new DateTime(2022, 2, 1, 12, 11, 10),
                    ModifiedDate = null,
                    ViewCount = 19,
                    Author = authors[3],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[5]
                    }
                },
                new()
                {
                    Title = "Cách Tạo vòng lặp vô hạn trên máy tính để Troll bạn bè",
                    ShortDescription = "Troll bạn bè",
                    Description = "Cách Tạo vòng lặp vô hạn trên máy tính để Troll bạn bè",
                    Meta = "",
                    UrlSlug = "cach-tao-vong-lap-vo-han-tren-may-tinh-de-troll-ban-be",
                    Published = true,
                    PostedDate = new DateTime(2022, 9, 8, 7, 6, 5),
                    ModifiedDate = null,
                    ViewCount = 20,
                    Author = authors[6],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[8]
                    }
                },
                new()
                {
                    Title = "Cách sửa lỗi máy tính tự khởi động lại liên tục",
                    ShortDescription = "Sửa lỗi",
                    Description = "Cách sửa lỗi máy tính tự khởi động lại liên tục",
                    Meta = "",
                    UrlSlug = "cach-sua-loi-may-tinh-tu-khoi-dong-lai-lien-tuc",
                    Published = true,
                    PostedDate = new DateTime(2022, 4, 3, 2, 1, 12),
                    ModifiedDate = null,
                    ViewCount = 21,
                    Author = authors[9],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[11]
                    }
                },
                new()
                {
                    Title = "Hướng dẫn tự thiết kế căn phòng bằng AI RoomGPT",
                    ShortDescription = "Thiết kế",
                    Description = "Hướng dẫn tự thiết kế căn phòng bằng AI RoomGPT",
                    Meta = "",
                    UrlSlug = "huong-dan-tu-thiet-ke-can-phong-bang-ai-roomgpt",
                    Published = true,
                    PostedDate = new DateTime(2022, 11, 10, 9, 8, 7),
                    ModifiedDate = null,
                    ViewCount = 22,
                    Author = authors[11],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[9]
                    }
                },
                new()
                {
                    Title = "Cách dùng AI của DALL-E 2 để tạo hình ảnh theo ý bạn",
                    ShortDescription = "DALL-E 2",
                    Description = "Cách dùng AI của DALL-E 2 để tạo hình ảnh theo ý bạn",
                    Meta = "",
                    UrlSlug = "cach-dung-ai-cua-dall-e-2-de-tao-hinh-anh-theo-y-ban",
                    Published = true,
                    PostedDate = new DateTime(2022, 6, 5, 4, 3, 2),
                    ModifiedDate = null,
                    ViewCount = 23,
                    Author = authors[8],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[6]
                    }
                },
                new()
                {
                    Title = "Hướng dẫn sử dụng Bing AI Chatbot của Microsoft",
                    ShortDescription = "Bing AI",
                    Description = "Hướng dẫn sử dụng Bing AI Chatbot của Microsoft",
                    Meta = "",
                    UrlSlug = "huong-dan-su-dung-bing-ai-chatbot-cua-microsoft",
                    Published = true,
                    PostedDate = new DateTime(2022, 1, 12, 11, 10, 9),
                    ModifiedDate = null,
                    ViewCount = 24,
                    Author = authors[5],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[3]
                    }
                },
                new()
                {
                    Title = "Cách tạo Web App Chatbot viết code tự động bằng Python",
                    ShortDescription = "Web App Chatbot",
                    Description = "Cách tạo Web App Chatbot viết code tự động bằng Python",
                    Meta = "",
                    UrlSlug = "cach-tao-web-app-chatbot-viet-code-tu-dong-bang-python",
                    Published = true,
                    PostedDate = new DateTime(2022, 8, 7, 6, 5, 4),
                    ModifiedDate = null,
                    ViewCount = 25,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "Những cách thay đổi kích thước icon trên màn hình Windows 10",
                    ShortDescription = "Thay đổi kích thước icon",
                    Description = "Những cách thay đổi kích thước icon trên màn hình Windows 10",
                    Meta = "",
                    UrlSlug = "nhung-cach-thay-doi-kich-thuoc-icon-tren-man-hinh-windows-10",
                    Published = true,
                    PostedDate = new DateTime(2022, 3, 2, 1, 12, 11),
                    ModifiedDate = null,
                    ViewCount = 26,
                    Author = authors[0],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title = "Tạo Fake Link và ảnh bài viết Facebook để Troll bạn bè",
                    ShortDescription = "Fake Link",
                    Description = "Tạo Fake Link và ảnh bài viết Facebook để Troll bạn bè",
                    Meta = "",
                    UrlSlug = "tao-fake-link-va-anh-bai-viet-facebook-de-troll-ban-be",
                    Published = true,
                    PostedDate = new DateTime(2022, 10, 9, 8, 7, 6),
                    ModifiedDate = null,
                    ViewCount = 27,
                    Author = authors[3],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[5]
                    }
                },
                new()
                {
                    Title = "Hướng dẫn dùng AI của Canva để vẽ ảnh bằng câu lệnh",
                    ShortDescription = "AI Canva",
                    Description = "Hướng dẫn dùng AI của Canva để vẽ ảnh bằng câu lệnh",
                    Meta = "",
                    UrlSlug = "huong-dan-dung-ai-cua-canva-de-ve-anh-bang-cau-lenh",
                    Published = true,
                    PostedDate = new DateTime(2022, 5, 4, 3, 2, 1),
                    ModifiedDate = null,
                    ViewCount = 28,
                    Author = authors[6],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[8]
                    }
                },
                new()
                {
                    Title = "Skybox – Xây dựng thế giới ảo của bạn bằng công cụ có sẵn",
                    ShortDescription = "Xây dựng thế giới ảo",
                    Description = "Skybox – Xây dựng thế giới ảo của bạn bằng công cụ có sẵn",
                    Meta = "",
                    UrlSlug = "skybox-xay-dung-the-gioi-ao-cua-ban-bang-cong-cu-co-san",
                    Published = true,
                    PostedDate = new DateTime(2022, 12, 9, 8, 7, 6),
                    ModifiedDate = null,
                    ViewCount = 29,
                    Author = authors[9],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[11]
                    }
                },
                new()
                {
                    Title = "Cách dùng Runway chỉnh sửa video bằng AI trong chớp mắt",
                    ShortDescription = "Chỉnh sửa video",
                    Description = "Cách dùng Runway chỉnh sửa video bằng AI trong chớp mắt",
                    Meta = "",
                    UrlSlug = "cach-dung-runway-chinh-sua-video-bang-ai-trong-chop-mat",
                    Published = true,
                    PostedDate = new DateTime(2022, 5, 4, 3, 2, 1),
                    ModifiedDate = null,
                    ViewCount = 30,
                    Author = authors[11],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[9]
                    }
                },
                new()
                {
                    Title = "Cách bật File Explorer Windows 11 mới bằng ViVeTool",
                    ShortDescription = "ViVeTool",
                    Description = "Cách bật File Explorer Windows 11 mới bằng ViVeTool",
                    Meta = "",
                    UrlSlug = "cach-bat-file-explorer-windows-11-moi-bang-vivetool",
                    Published = true,
                    PostedDate = new DateTime(2022, 12, 11, 10, 9, 8),
                    ModifiedDate = null,
                    ViewCount = 31,
                    Author = authors[8],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[6]
                    }
                },
                new()
                {
                    Title = "Cách dùng Notion AI để tự động viết Blog, thơ, luận văn, đồ án",
                    ShortDescription = "Viết tự động",
                    Description = "Cách dùng Notion AI để tự động viết Blog, thơ, luận văn, đồ án",
                    Meta = "",
                    UrlSlug = "cach-dung-notion-ai-de-tu-dong-viet-blog-tho-luan-van-do-an",
                    Published = true,
                    PostedDate = new DateTime(2022, 7, 6, 5, 4, 3),
                    ModifiedDate = null,
                    ViewCount = 32,
                    Author = authors[5],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[3]
                    }
                },
                new()
                {
                    Title = "Cách dùng AI của PlaygroundAI để chỉnh sửa ảnh không cần Photoshop",
                    ShortDescription = "",
                    Description = "Cách dùng AI của PlaygroundAI để chỉnh sửa ảnh không cần Photoshop",
                    Meta = "",
                    UrlSlug = "cach-dung-ai-cua-playgroundai-de-chinh-sua-anh-khong-can-photoshop",
                    Published = true,
                    PostedDate = new DateTime(2022, 2, 1, 12, 11, 10),
                    ModifiedDate = null,
                    ViewCount = 33,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
                new()
                {
                    Title = "Cách vượt Captcha Google bằng python",
                    ShortDescription = "Vượt Captcha",
                    Description = "Cách vượt Captcha Google bằng python",
                    Meta = "",
                    UrlSlug = "cach-vuot-captcha-google-bang-python",
                    Published = true,
                    PostedDate = new DateTime(2022, 9, 8, 7, 6, 5),
                    ModifiedDate = null,
                    ViewCount = 34,
                    Author = authors[0],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title = "Kiếm Voucher Grab, Shopee… với chương trình Microsoft Rewards",
                    ShortDescription = "",
                    Description = "Kiếm Voucher Grab, Shopee… với chương trình Microsoft Rewards",
                    Meta = "",
                    UrlSlug = "kiem-voucher-grab-shopee-voi-chuong-trinh-microsoft-rewards",
                    Published = true,
                    PostedDate = new DateTime(2022, 4, 3, 2, 1, 12),
                    ModifiedDate = null,
                    ViewCount = 35,
                    Author = authors[3],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[5]
                    }
                },
                new()
                {
                    Title = "Cách đăng ký Mate Translate Pro miễn phí vĩnh viển",
                    ShortDescription = "Mate Translate Pro",
                    Description = "Cách đăng ký Mate Translate Pro miễn phí vĩnh viển",
                    Meta = "",
                    UrlSlug = "cach-dang-ky-mate-translate-pro-mien-phi-vinh-vien",
                    Published = true,
                    PostedDate = new DateTime(2022, 11, 10, 9, 8, 7),
                    ModifiedDate = null,
                    ViewCount = 36,
                    Author = authors[6],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[8]
                    }
                },
                new()
                {
                    Title = "Futurepedia – Trang web tổng hợp hơn 1000 công cụ AI",
                    ShortDescription = "Futurepedia",
                    Description = "Futurepedia – Trang web tổng hợp hơn 1000 công cụ AI",
                    Meta = "",
                    UrlSlug = "futurepedia-trang-web-tong-hop-hon-1000-cong-cu-ai",
                    Published = true,
                    PostedDate = new DateTime(2022, 6, 5, 4, 3, 2),
                    ModifiedDate = null,
                    ViewCount = 37,
                    Author = authors[9],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[11]
                    }
                },
                new()
                {
                    Title = "FastForward: Download File nhanh ở các trang rút gọn Link",
                    ShortDescription = "FastForward",
                    Description = "FastForward: Download File nhanh ở các trang rút gọn Link",
                    Meta = "",
                    UrlSlug = "fastforward-download-file-nhanh-o-cac-trang-rut-gon-link",
                    Published = true,
                    PostedDate = new DateTime(2022, 1, 12, 11, 10, 9),
                    ModifiedDate = null,
                    ViewCount = 38,
                    Author = authors[11],
                    Category = categories[10],
                    Tags = new List<Tag>()
                    {
                        tags[9]
                    }
                },
                new()
                {
                    Title = "Cách đăng ký các khóa học miễn phí trên Coursera",
                    ShortDescription = "Coursera",
                    Description = "Cách đăng ký các khóa học miễn phí trên Coursera",
                    Meta = "",
                    UrlSlug = "cach-dang-ky-cac-khoa-hoc-mien-phi-tren-coursera",
                    Published = true,
                    PostedDate = new DateTime(2022, 8, 7, 6, 5, 4),
                    ModifiedDate = null,
                    ViewCount = 39,
                    Author = authors[8],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[6]
                    }
                },
            };

            _dbContext.AddRange(posts);
            _dbContext.SaveChanges();

            return posts;
        }

        private IList<Tag> AddTags()
        {
            var tags = new List<Tag>()
            {
                new() { Name = "Google", Description = "Google application", UrlSlug = "google" },
                new() { Name = "ASP.NET MVC", Description = "ASP.NET MVC", UrlSlug = "asp-dot-net-mvc" },
                new() { Name = "Razor Page", Description = "Razor Page", UrlSlug = "razor-page" },
                new() { Name = "Blazor", Description = "Blazor", UrlSlug = "blazor" },
                new() { Name = "Deep Learning", Description = "Deep Learning", UrlSlug = "deep-learning" },
                new() { Name = "Neural Network", Description = "Neural Network", UrlSlug = "neural-network" },
                new() { Name = "IOBIT", Description = "IOBIT", UrlSlug = "iobit" },
                new() { Name = "GTPGO", Description = "GTPGO", UrlSlug = "gtpgo" },
                new() { Name = "iRocketVPN", Description = "iRocketVPN", UrlSlug = "irocketvpn" },
                new() { Name = "NextDNS", Description = "NextDNS", UrlSlug = "nextdns" },
                new() { Name = "AOMEI", Description = "AOMEI", UrlSlug = "aomei" },
                new() { Name = "GlotDojo", Description = "GlotDojo", UrlSlug = "glotdojo" },
                new() { Name = "Capture2Text", Description = "Capture2Text", UrlSlug = "capture2text" },
                new() { Name = "HoneCtrl", Description = "HoneCtrl", UrlSlug = "honectrl" },
                new() { Name = "iTopVPN", Description = "iTopVPN", UrlSlug = "itopvpn" },
                new() { Name = "Red Team", Description = "Red Team", UrlSlug = "red-team" },
                new() { Name = "Dalfox", Description = "Dalfox", UrlSlug = "dalfox" },
                new() { Name = "Ghunt V2", Description = "Ghunt V2", UrlSlug = "ghunt-v2" },
                new() { Name = "Brute Force", Description = "Brute Force", UrlSlug = "brute-force" },
                new() { Name = "Defgen", Description = "Defgen", UrlSlug = "defgen" },
                new() { Name = "Event DDos", Description = "Event DDos", UrlSlug = "event-ddos" },
                new() { Name = "Tor", Description = "Tor", UrlSlug = "tor" },
                new() { Name = "SpiderFoot", Description = "SpiderFoot", UrlSlug = "spiderfoot" },
                new() { Name = "OSINT", Description = "OSINT", UrlSlug = "osint" },
                new() { Name = "Skynet", Description = "Skynet", UrlSlug = "skynet" },
                new() { Name = "SMS OTP V3", Description = "SMS OTP V3", UrlSlug = "sms-otp-v3" }
            };

            _dbContext.AddRange(tags);
            _dbContext.SaveChanges();

            return tags;
        }

        private IList<Category> AddCategories()
        {
            var categories = new List<Category>()
            {
                new() { Name = ".NET Core", Description = ".NET Core", UrlSlug="dot-net-core" },
                new() { Name = "Architecture", Description = "Architecture", UrlSlug="architecture" },
                new() { Name = "Messaging", Description = "Messaging", UrlSlug="messaging" },
                new() { Name = "OOP", Description = "Object Oriented Programming", UrlSlug="oop" },
                new() { Name = "Design Patterns", Description = "Design Patterns", UrlSlug="design-patterns" },
                new() { Name = "Bootstrap", Description = "Bootstrap", UrlSlug="bootstrap" },
                new() { Name = "SQL Server", Description = "SQL Server", UrlSlug="sql-server" },
                new() { Name = "JavaScript", Description = "JavaScript", UrlSlug="javascript" },
                new() { Name = "PHP", Description = "PHP", UrlSlug="php" },
                new() { Name = "JQuery", Description = "JQuery", UrlSlug="jquery" },
                new() { Name = "MongoDB", Description = "MongoDB", UrlSlug="mongodb" },
                new() { Name = "Unix/ Linux", Description = "Unix/ Linux", UrlSlug="unix-linux" },
                new() { Name = "Git", Description = "Git", UrlSlug="git" },
                new() { Name = "NodeJS", Description = "NodeJS", UrlSlug="nodejs" },
                new() { Name = "SQL Injection", Description = "SQL Injection", UrlSlug="sql-injection" }
            };

            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();

            return categories;
        }

        private IList<Author> AddAuthors()
        {
            var authors = new List<Author>()
            {
                new()
                {
                    FullName = "Jason Mouth",
                    UrlSlug = "jason-mouth",
                    Email = "json@gmail.com",
                    JoinedDate = new DateTime(2022, 12, 10)
                },
                new()
                {
                    FullName = "Jessica Wonder",
                    UrlSlug = "jessica-wonder",
                    Email = "jessica665@motip.com",
                    JoinedDate = new DateTime(2020, 11, 10)
                },
                new()
                {
                    FullName = "Kathy Smith",
                    UrlSlug = "kathy-smith",
                    Email = "kathy.smith@iworld.com",
                    JoinedDate = new DateTime(2010, 9, 6)
                },
                new()
                {
                    FullName = "Huỳnh Tấn Thanh",
                    UrlSlug = "huynh-tan-thanh",
                    Email = "nigga@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 10, 9)
                },
                new()
                {
                    FullName = "Đặng Ngọc Thắng",
                    UrlSlug = "dang-ngoc-thang",
                    Email = "orange@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 8, 7)
                },
                new()
                {
                    FullName = "Đoàn Cao Nhật Hạ",
                    UrlSlug = "doan-cao-nhat-ha",
                    Email = "summer@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 6, 5)
                },
                new()
                {
                    FullName = "Bùi Văn Du",
                    UrlSlug = "bui-van-du",
                    Email = "three@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 4, 3)
                },
                new()
                {
                    FullName = "Nguyễn Tuấn Kiệt",
                    UrlSlug = "nguyen-tuan-kiet",
                    Email = "alumica@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 2, 1)
                },
                new()
                {
                    FullName = "Tác giả mới 1",
                    UrlSlug = "tac-gia-moi-1",
                    Email = "tacgiamoi1@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 12, 11)
                },
                new()
                {
                    FullName = "Tác giả mới 2",
                    UrlSlug = "tac-gia-moi-2",
                    Email = "tacgiamoi2@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 10, 9)
                },
                new()
                {
                    FullName = "Tác giả mới 3",
                    UrlSlug = "tac-gia-moi-3",
                    Email = "tacgiamoi3@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 8, 7)
                },
                new()
                {
                    FullName = "Tác giả mới 4",
                    UrlSlug = "tac-gia-moi-4",
                    Email = "tacgiamoi4@dlu.edu.vn",
                    JoinedDate = new DateTime(2022, 6, 5)
                }
            };

            _dbContext.Authors.AddRange(authors);
            _dbContext.SaveChanges();

            return authors;
        }
    }
}
