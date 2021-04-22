
    $(function () {
        $('#btnUpload').click(function () {
            $('#fileUpload').trigger('click');
        });
            $('#fileUpload').change(function () {
                if (window.FormData !== undefined) {
                    var fileUpload = $('#fileUpload').get(0);
                    var files = fileUpload.files;
                    var formDt = new FormData();
                    formDt.append('file', files[0]);
                    $.ajax({
                        type: 'POST',
                        url: '/AdminManagement/Upload',
                        contentType: false, // khong co header
                        processData: false, // khong xu ly du lieu
                        data: formDt,
                        success: function (urlImage) {
                            alert('Upload file:' + urlImage + " thành công!");
                            $('#pictureUpload').attr('src', urlImage);
                            $('#picture').val(urlImage);
                        },
                        error: function (err) {
                            alert('Có lỗi khi upload: ' + err.statusText);
                        }
                    });
                }
            });
        });
