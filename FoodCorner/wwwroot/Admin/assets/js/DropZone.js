$(function () {

    $('#dropzone').on('dragover', function () {
        $(this).addClass('hover');
    });

    $('#dropzone').on('dragleave', function () {
        $(this).removeClass('hover');
    });

    $('#dropzone input').on('change', function (e) {
        var file = this.files[0];

        $('#dropzone').removeClass('hover');



        $('#dropzone').addClass('dropped');
        $('#dropzone img').remove();

        if ((/^image\/(gif|png|jpeg)$/i).test(file.type)) {
            var reader = new FileReader(file);

            reader.readAsDataURL(file);

            reader.onload = function (e) {
                var data = e.target.result,
                    $img = $('<img />').attr('src', data).fadeIn();

                $('#dropzone div').html($img);
            };
        } else {
            var ext = file.name.split('.').pop();

            $('#dropzone div').html(ext);
        }
    });
});

$(function () {

    $('#Posterdropzone').on('dragover', function () {
        $(this).addClass('hover');
    });

    $('#Posterdropzone').on('dragleave', function () {
        $(this).removeClass('hover');
    });

    $('#Posterdropzone input').on('change', function (e) {
        var file = this.files[0];

        $('#Posterdropzone').removeClass('hover');



        $('#Posterdropzone').addClass('dropped');
        $('#Posterdropzone img').remove();

        if ((/^image\/(gif|png|jpeg)$/i).test(file.type)) {
            var reader = new FileReader(file);

            reader.readAsDataURL(file);

            reader.onload = function (e) {
                var data = e.target.result,
                    $img = $('<img />').attr('src', data).fadeIn();

                $('#Posterdropzone div').html($img);
            };
        } else {
            var ext = file.name.split('.').pop();

            $('#Posterdropzone div').html(ext);
        }
    });
});