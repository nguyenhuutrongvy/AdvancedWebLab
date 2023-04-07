import React, { useEffect } from "react";

const Contact = () => {
    useEffect(() => {
        document.title = 'Liên hệ';
    }, []);

    return (
        <main role="main">
            <div class="container mt-2">
                <h1 class="text-center">Tips & Tricks Blog</h1>
                <div class="row">
                    <div class="col col-md-6">
                        <img src="../../public/images/image_2.jpg" />
                    </div>
                    <div class="col col-md-6">
                        <form method="post">
                            <div class="form-group">
                                <label for="email">Email của bạn</label>
                                <input type="email" class="form-control" id="email" name="email"
                                    placeholder="Email của bạn" />
                            </div>
                            <div class="form-group">
                                <label for="title">Tiêu đề của bạn</label>
                                <input type="text" class="form-control" id="title" name="title"
                                    placeholder="Tiêu đề của bạn" />
                            </div>
                            <div class="form-group">
                                <label for="message">Lời nhắn của bạn</label>
                                <textarea name="message" class="form-control"></textarea>
                            </div>
                            <button class="btn btn-primary" name="btnGuiLoiNhan">Gửi lời nhắn</button>
                        </form>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col col-md-12">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d124928.4678792054!2d108.38068208547176!3d11.90407018569766!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x317112fef20988b1%3A0xad5f228b672bf930!2zVHAuIMSQw6AgTOG6oXQsIEzDom0gxJDhu5NuZywgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2sus!4v1678456920102!5m2!1svi!2sus" width="100%" height="450" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </div>
            </div>
        </main>
    );
}

export default Contact;