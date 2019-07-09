#import <Foundation/Foundation.h>


NS_ASSUME_NONNULL_BEGIN

@interface XGZIP : NSObject

- (NSData *)gzippedDataWithCompressionLevel:(float)level data:(NSData*) data;
- (NSData *)gzippedData:(NSData*) _data;
- (NSData *)gunzippedData:(NSData*) _data;

NS_ASSUME_NONNULL_END
@end
