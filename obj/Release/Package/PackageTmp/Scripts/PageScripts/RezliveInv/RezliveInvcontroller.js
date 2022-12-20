
app.service("ProcessResultService", function ($http) {
    this.getAllProcessResult = function (Type) {
        var promise = $http({
            url: '/Home/GetProcessResult',
            method: 'POST',
            data: {
                strType: Type
            }
        });
        return promise;
    };

    this.getBatchNo = function (Type) {
        var promise = $http({
            url: '/Home/GetBatchNo',
            method: 'POST',
            data: {
                strType: Type
            }
        });
        return promise;
    };

    this.BatchDelete = function (BatchNo) {
        var promise = $http({
            url: '/Home/BatchDelete',
            method: 'POST',
            data: {
                strBatchNo: BatchNo
            }
        });
        return promise;
    };

    this.ExportByDate = function (Date, type) {
       
        var promise = $http({
            url: '/Home/ExportQnetlnv',
            method: 'POST',
            data: {
                strType: type, strDate: Date
            }
        });
        return promise;
    };

    this.getAllHistory = function () {
        var promise = $http({
            url: '/Home/GetAllHistory',
            method: 'POST'           
        });
        return promise;
    };
});

app.controller('Historycontroller', function ($scope, $uibModal, ProcessResultService) {



    $scope.initialize = function () {
        $scope.History = [];
        $scope.loadHistory();     
    }

    $scope.loadHistory = function () {
        ProcessResultService.getAllHistory().then(function (response) {
            $scope.History = response.data;          
        });
    }
});

app.controller('RezliveInvcontroller', function ($scope, $uibModal, ProcessResultService) {
    var RezliveInvsData = [];
    var varData = null;
    var varDiff = null;
    $scope.loadRezliveInv = function () {
        ProcessResultService.getAllProcessResult('RezliveInv').then(function (response) {
            $scope.RezliveInvs = response.data;
            RezliveInvsData = response.data;
            varData = RezliveInvsData.filter(function (RezliveInvsData) {
                return RezliveInvsData.CreatedDateTime == $scope.FDateTimes;
            });
            $scope.totalItems = (varData.length / '2');
        });
    }

    $scope.loadBatchNo = function () {
        ProcessResultService.getBatchNo('RezliveInv').then(function (response) {
            $scope.BatchNo = response.data;
        });
    }

    $scope.$watch('FDateTime', function () {
        var vardate = $scope.FDateTime;
        $scope.FDateTimes = moment(vardate).format('DD/MM/YYYY');

        $scope.searchTexts = '';
        $scope.loadRezliveInv();
    });
    $scope.initialize = function () {

        $scope.RezliveInvs = [];
        $scope.loadRezliveInv();     
        $scope.BatchNo = [];      
        $scope.loadBatchNo();
    }
   

    $scope.currentPage = 1;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };
    $scope.pageChanged = function () {

        if ($scope.FDateTimes !== '') {
            varData = RezliveInvsData.filter(function (RezliveInvsData) {
                return RezliveInvsData.CreatedDateTime == $scope.FDateTimes;
            });



        }
        var pagedData = varData.slice(
            ($scope.currentPage - 1) * $scope.maxSize,
            $scope.currentPage * $scope.maxSize
        );
        $scope.RezliveInvs = pagedData;
    };


    $scope.maxSize = 15;
    $scope.bigCurrentPage = 1;

    $scope.BatchNofilter = function () {
        $scope.loadRezliveInv();
        $scope.searchTexts = $scope.BatchNos;
        $scope.FDateTimes = '';

        var varQdata = RezliveInvsData.filter(function (RezliveInvsData) {
            return RezliveInvsData.BatchNo == $scope.BatchNos;
        });

        RezliveInvsData = varQdata;
        varData = varQdata;
        $scope.totalItems = (varQdata.length / '2');
    }

    $scope.deleteBatch = function (BatchNos) {
        if (BatchNos == undefined) {
            alert('Please select Batch No')
        }
        else {
            ProcessResultService.BatchDelete(BatchNos).then(function (resposne) {
                if (resposne !== undefined) {
                    if (resposne.data === '"success"') {
                        //reset();
                        $scope.initialize();
                        alert('successFully Deleted');
                        //window.location.href = "/";
                    }
                    else if (resposne.data === "error")
                        alert(resposne.data.Message);
                }
            });
        }

    };
    $scope.Export = function () {
        var m = new Date();
        var dateString =
            m.getUTCFullYear() + "/" +
            ("0" + (m.getUTCMonth() + 1)).slice(-2) +
            ("0" + m.getUTCDate()).slice(-2) +
            ("0" + m.getUTCHours()).slice(-2) + 
            ("0" + m.getUTCMinutes()).slice(-2) + 
            ("0" + m.getUTCSeconds()).slice(-2);
        $('#rezlivInvTbl').table2excel({
           
            filename: dateString+"_"+"RezliveInv"
        });
    }

    $scope.Diff = function () {
       
         varDiff = RezliveInvsData.filter(function (RezliveInvsData) {
            return RezliveInvsData.AmountDiff > 0.00;
         });
       
        $scope.RezliveInvs = varDiff;
    }
    $scope.ExportByDate = function () {
        ProcessResultService.ExportByDate($scope.FDateTimes, 'RezliveInv').then(function (resposne) {
        });
    }
  
});

app.service("QnetlnvService", function ($http) {
    this.getAllQnetlnv = function (Type) {
        var promise = $http({
            url: '/Home/GetImportDoc',
            method: 'POST',
            data: {
                strType: Type
            }
        });
        return promise;
    };

});

app.controller('Qnetlnvcontroller', function ($scope, $uibModal, QnetlnvService, ProcessResultService) {
    var QnetlnvsData = [];
    var varData = null;
    var varDiff = null;
    $scope.loadQnetlnv = function () {
        ProcessResultService.getAllProcessResult('Qnetlnv').then(function (response) {
            $scope.Qnetlnvs = response.data;
            QnetlnvsData = response.data;
             varData = QnetlnvsData.filter(function (QnetlnvsData) {
                return QnetlnvsData.CreatedDateTime == $scope.FDateTimes;
            });
            $scope.totalItems = (varData.length / '2');
        });
    }

    $scope.loadBatchNo = function () {
        ProcessResultService.getBatchNo('Qnetlnv').then(function (response) {
            $scope.BatchNo = response.data;
        });
    }
    $scope.initialize = function () {

        $scope.Qnetlnvs = [];
        $scope.BatchNo = [];
        $scope.loadQnetlnv();
        $scope.loadBatchNo();
        
    }
    $scope.$watch('FDateTime', function () {
        var vardate = $scope.FDateTime;
        $scope.FDateTimes = moment(vardate).format('DD/MM/YYYY');

        $scope.searchTexts = '';
        $scope.loadQnetlnv();
    });
    $scope.$watch('searchText', function () {
        var vardate = $scope.searchText;
        $scope.searchTexts = vardate;

    });

    $scope.currentPage = 1;
   
    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };
    $scope.pageChanged = function () {

        if ($scope.FDateTimes !== '') {
            varData = QnetlnvsData.filter(function (QnetlnvsData) {
                return QnetlnvsData.CreatedDateTime == $scope.FDateTimes;
            });

          

        }
        var pagedData = varData.slice(
            ($scope.currentPage - 1) * $scope.maxSize,
            $scope.currentPage * $scope.maxSize
        );
        $scope.Qnetlnvs = pagedData;
    };
        
       
    $scope.maxSize = 15;
    $scope.bigCurrentPage = 1;

    $scope.BatchNofilter = function () {
        $scope.loadQnetlnv();
        $scope.searchTexts = $scope.BatchNos;
        $scope.FDateTimes = '';
     
        var varQdata = QnetlnvsData.filter(function (QnetlnvsData) {
            return QnetlnvsData.BatchNo == $scope.BatchNos;
        });
        
        QnetlnvsData = varQdata;
        varData = varQdata;
        $scope.totalItems = (varQdata.length / '2');
    }

    $scope.deleteBatch = function (BatchNos) {
        if (BatchNos == undefined) {
            alert('Please select Batch No')
        }
        else {
            ProcessResultService.BatchDelete(BatchNos).then(function (resposne) {
                if (resposne !== undefined) {
                    if (resposne.data === '"success"') {
                        //reset();
                        $scope.initialize();
                        alert('successFully Deleted');
                        //window.location.href = "/";
                    }
                    else if (resposne.data === "error")
                        alert(resposne.data.Message);
                }
            });
        }
       
    };

    $scope.Export = function () {
        var m = new Date();
        var dateString =
            m.getUTCFullYear() + "/" +
            ("0" + (m.getUTCMonth() + 1)).slice(-2) +
            ("0" + m.getUTCDate()).slice(-2) +
            ("0" + m.getUTCHours()).slice(-2) +
            ("0" + m.getUTCMinutes()).slice(-2) +
            ("0" + m.getUTCSeconds()).slice(-2);
        $('#Qnetlnvtbl').table2excel({

            filename: dateString + "_" + "Qnetlnv"
        });
    }

    $scope.Diff = function () {
        
        varDiff = QnetlnvsData.filter(function (QnetlnvsData) {
            return QnetlnvsData.AmountDiff > 0.00;
        });

        $scope.Qnetlnvs = varDiff;
    }
    $scope.ExportByDate = function () {
        ProcessResultService.ExportByDate($scope.FDateTimes,'Qnetlnv').then(function (resposne) {
        });
    }
});
app.controller('EcardInvcontroller', function ($scope, $uibModal, ProcessResultService) {

    var EcardInvsData = [];
    var varData = null;
    var varDiff = null;
    $scope.loadEcardInv = function () {
        ProcessResultService.getAllProcessResult('EcardInv').then(function (response) {
            $scope.EcardInvs = response.data;
            EcardInvsData = response.data;
            varData = EcardInvsData.filter(function (EcardInvsData) {
                return EcardInvsData.CreatedDateTime == $scope.FDateTimes;
            });
            $scope.totalItems = (varData.length / '2');
        });
    }

    $scope.loadBatchNo = function () {
        ProcessResultService.getBatchNo('EcardInv').then(function (response) {
            $scope.BatchNo = response.data;
        });
    }

    $scope.$watch('FDateTime', function () {
        var vardate = $scope.FDateTime;
        $scope.FDateTimes = moment(vardate).format('DD/MM/YYYY');

        $scope.searchTexts = '';
        $scope.loadEcardInv();
    });
    $scope.initialize = function () {

        $scope.EcardInvs = [];
        $scope.loadEcardInv();
        $scope.BatchNo = [];
        $scope.loadBatchNo();
    }
   
    $scope.currentPage = 1;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };
    $scope.pageChanged = function () {

        if ($scope.FDateTimes !== '') {
            varData = EcardInvsData.filter(function (EcardInvsData) {
                return EcardInvsData.CreatedDateTime == $scope.FDateTimes;
            });



        }
        var pagedData = varData.slice(
            ($scope.currentPage - 1) * $scope.maxSize,
            $scope.currentPage * $scope.maxSize
        );
        $scope.EcardInvs = pagedData;
    };


    $scope.maxSize = 15;
    $scope.bigCurrentPage = 1;

    $scope.BatchNofilter = function () {
        $scope.loadEcardInv();
        $scope.searchTexts = $scope.BatchNos;
        $scope.FDateTimes = '';

        var varQdata = EcardInvsData.filter(function (EcardInvsData) {
            return EcardInvsData.BatchNo == $scope.BatchNos;
        });

        EcardInvsData = varQdata;
        varData = varQdata;
        $scope.totalItems = (varQdata.length / '2');
    }

    $scope.deleteBatch = function (BatchNos) {
        if (BatchNos == undefined) {
            alert('Please select Batch No')
        }
        else {
            ProcessResultService.BatchDelete(BatchNos).then(function (resposne) {
                if (resposne !== undefined) {
                    if (resposne.data === '"success"') {
                        //reset();
                        $scope.initialize();
                        alert('successFully Deleted');
                        //window.location.href = "/";
                    }
                    else if (resposne.data === "error")
                        alert(resposne.data.Message);
                }
            });
        }

    };
    $scope.Export = function () {
        var m = new Date();
        var dateString =
            m.getUTCFullYear() + "/" +
            ("0" + (m.getUTCMonth() + 1)).slice(-2) +
            ("0" + m.getUTCDate()).slice(-2) +
            ("0" + m.getUTCHours()).slice(-2) +
            ("0" + m.getUTCMinutes()).slice(-2) +
            ("0" + m.getUTCSeconds()).slice(-2);
        $('#EcardInvtbl').table2excel({

            filename: dateString + "_" + "EcardInv"
        });
    }

    $scope.Diff = function () {
      
        varDiff = EcardInvsData.filter(function (EcardInvsData) {
            return EcardInvsData.AmountDiff > 0.00;
        });

        $scope.EcardInvs = varDiff;
    }
    $scope.ExportByDate = function () {
        ProcessResultService.ExportByDate($scope.FDateTimes, 'EcardInv').then(function (resposne) {
        });
    }
});

angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache']).controller('AppCtrl', function () {
    this.myDate = new Date();
    this.isOpen = false;
});